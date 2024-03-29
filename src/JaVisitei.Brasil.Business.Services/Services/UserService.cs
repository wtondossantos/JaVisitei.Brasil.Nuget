﻿using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.ViewModels.Response.User;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Helper.Others;

using System.Threading.Tasks;
using AutoMapper;
using System;
using JaVisitei.Brasil.Business.ViewModels.Request.Recaptcha;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserManagerService _userManagerService;
        private readonly IUserRepository _userRepository;
        private readonly UserValidator _userValidator;
        private readonly IEmailService _emailService;
        private readonly IRecaptchaService _recaptchaService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
            IUserManagerService userManagerService,
            UserValidator userValidator,
            IEmailService emailService,
            IRecaptchaService recaptchaService,
            IMapper mapper) : base(userRepository, mapper)
        {
            _userManagerService = userManagerService;
            _userRepository = userRepository;
            _userValidator = userValidator;
            _emailService = emailService;
            _recaptchaService = recaptchaService;
            _mapper = mapper;
        }

        public async Task<UserValidator> InsertAsync(InsertUserRequest request)
        {
            return await InsertAsync(_mapper.Map<InsertFullUserRequest>(request));
        }

        public async Task<UserValidator> InsertAsync(InsertFullUserRequest request)
        {
            try
            {
                var validate = _recaptchaService.RetrieveAsync(new RecaptchaRequest { Key = request.Key, Response = request.Recaptcha });

                if (validate is null)
                    return null;

                if (validate.Result is not null && !validate.Result.IsValid && validate.Result.Data is not null && !validate.Result.Data.Success)
                    return new UserValidator { Errors = validate.Result.Errors };

                _userValidator.ValidatesUserCreation(request);

                if (!_userValidator.IsValid)
                    return _userValidator;

                if (await _userRepository.AnyAsync(x => x.Email.Equals(request.Email) || x.Username.Equals(request.Username)))
                {
                    _userValidator.Errors.Add("Já existe usuário com este e-mail e/ou usuário.");
                    return _userValidator;
                }

                var userMapper = _mapper.Map<User>(request);

                while (await _userRepository.AnyAsync(x => x.Id.Equals(userMapper.Id))) 
                {
                    userMapper.Id = Guid.NewGuid().ToString();
                }

                if (await _userRepository.InsertAsync(userMapper))
                {
                    var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email.Equals(userMapper.Email));
                    if (user is null)
                    {
                        _userValidator.Errors.Add("Erro ao consultar usuário por e-mail.");
                        return _userValidator;
                    }

                    var userManager = await _userManagerService.CreateEmailConfirmationAsync(user.Id);
                    if (userManager is null)
                    {
                        await _userRepository.DeleteByIdAsync(user.Id);
                        _userValidator.Errors.Add("Erro ao adicionar configuração do usuário, tente novamente ou entre em contato com o suporte.");
                        return _userValidator;
                    }

                    userManager.User = user;
                    var emailResult = await _emailService.SendEmailUserManagerAsync(userManager);
                    if (emailResult.IsValid)
                    {
                        _userValidator.Data = _mapper.Map<UserResponse>(user);
                        _userValidator.Message = $"Usuário {request.Username}, {request.Email} registrado com sucesso. {emailResult.Message}";
                    }
                    else
                    {
                        await _userManagerService.DeleteByIdAsync(userManager.Id);
                        await _userRepository.DeleteByIdAsync(user.Id);
                        _userValidator.Errors = emailResult.Errors;
                        _userValidator.Errors.Add("Tente novamente.");
                    }
                }
                else
                    _userValidator.Errors.Add("Erro ao registrar usuário.");
            }
            catch
            {
                _userValidator.Errors.Add($"Erro ao registrar usuário");
                throw;
            }

            return _userValidator;
        }

        public async Task<UserValidator> UpdateAsync(UpdateUserRequest request)
        {
            return await UpdateAsync(_mapper.Map<UpdateFullUserRequest>(request));
        }

        public async Task<UserValidator> UpdateAsync(UpdateFullUserRequest request)
        {
            try
            {
                _userValidator.ValidatesUserEdition(request);

                if (!_userValidator.IsValid)
                    return _userValidator;

                if (await _userRepository.AnyAsync(x => x.Id != request.Id && x.Username.Equals(request.Username)))
                {
                    _userValidator.Errors.Add("Já existe usuário cadastrado com esse nome de usuário.");
                    return _userValidator;
                }

                if (await _userRepository.AnyAsync(x => x.Id != request.Id && x.Email.Equals(request.Email)))
                {
                    _userValidator.Errors.Add("Já existe usuário cadastrado com esse e-mail.");
                    return _userValidator;
                }

                var userFound = await _userRepository.GetFirstOrDefaultAsync(x => x.Id.Equals(request.Id));
                if (userFound is null)
                {
                    _userValidator.Errors.Add("Usuário não encontrado.");
                    return _userValidator;
                }

                if (!userFound.SecurityStamp.Equals(request.SecurityStamp))
                {
                    _userValidator.Errors.Add("Erro ao tentar atualizar usuário, carregue a tela e tente novamente.");
                    return _userValidator;
                }

                if (!string.IsNullOrEmpty(request.OldPassword))
                {
                    var oldPasswordHash = Encrypt.Sha256encrypt(request.OldPassword);

                    if (string.IsNullOrEmpty(userFound.Password) || userFound.Password != oldPasswordHash)
                    {
                        _userValidator.Errors.Add("Senha antiga incorreta.");
                        return _userValidator;
                    }

                    if (await _userRepository.LoginAsync(userFound.Email, oldPasswordHash) is null)
                    {
                        _userValidator.Errors.Add("E-mail ou Senha antiga incorreto.");
                        return _userValidator;
                    }
                }

                var userMapper = _mapper.Map<User>(request);
                if (userMapper?.Password is null)
                {
                    _userValidator.Errors.Add("Erro ao atualizar usuário.");
                    return _userValidator;
                }

                userMapper.Password = string.IsNullOrEmpty(request.Password) ? userFound.Password : Encrypt.Sha256encrypt(request.Password);
                userMapper.Actived = userFound.Actived;
                userMapper.RefreshToken = userFound.RefreshToken;

                if (await _userRepository.UpdateAsync(userMapper))
                {
                    var user = await _userRepository.GetByIdAsync(userMapper.Id);
                    if (user is not null)
                    {
                        _userValidator.Data = _mapper.Map<UserResponse>(user);
                        _userValidator.Message = $"Usuário {request.Username}, {request.Email} atualizado com sucesso.";
                    }
                    else
                        _userValidator.Errors.Add("Erro ao consultar usuário atualizado.");
                }
                else
                    _userValidator.Errors.Add("Erro ao atualizar usuário.");
            }
            catch
            {
                _userValidator.Errors.Add($"Erro ao atualizar usuário");
                throw;
            }

            return _userValidator;
        }

        public async Task<M> LoginAsync<M>(string email, string password)
        {
            var item = await _userRepository.LoginAsync(email, password);
            return item is null ? default : _mapper.Map<M>(item);
        }

        public async Task<M> RefreshTokenAsync<M>(string email, string refreshToken)
        {
            var item = await _userRepository.GetRefreshTokenAsync(email, refreshToken);
            return item is null ? default : _mapper.Map<M>(item);
        }
    }
}