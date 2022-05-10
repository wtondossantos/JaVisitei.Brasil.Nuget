using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Security;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Response.User;
using JaVisitei.Brasil.Business.ViewModels.Request.Email;
using JaVisitei.Brasil.Business.Enums;
using JaVisitei.Brasil.Helper.Others;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserManagerRepository _userManagerRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserValidator _userValidator;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
            IUserManagerRepository userManagerRepository,
            UserValidator userValidator,
            IEmailService emailService,
            IMapper mapper) : base(userRepository)
        {
            _userManagerRepository = userManagerRepository;
            _userRepository = userRepository;
            _userValidator = userValidator;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<UserValidator> AddAsync(AddUserRequest request)
        {
            var userAdmin = _mapper.Map<AddFullUserRequest>(request);
            return await AddAsync(userAdmin);
        }

        public async Task<UserValidator> AddAsync(AddFullUserRequest request)
        {
            try
            {
                _userValidator.ValidatesUserCreation(request);

                if (!_userValidator.IsValid)
                    return _userValidator;

                var userExists = await _userRepository.GetAsync(x => x.Email.Equals(request.Email) || x.Username.Equals(request.Username));
                if (userExists.ToList().Count > 0)
                    _userValidator.Errors.Add("Já existe usuário com este e-mail e/ou usuário.");
                else
                {
                    var userMapper = _mapper.Map<User>(request);
                    var userResult = await _userRepository.AddAsync(userMapper);
                    
                    if (userResult)
                    {
                        var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email.Equals(userMapper.Email));

                        var userManagerResponse = new UserManager
                        {
                            UserId = user.Id,
                            EmailId = (int)EmailEnum.ConfirmationEmail,
                            ManagerCode = TokenString.GenerateEmailConfirmationToken(),
                            ExpirationDate = DateTime.Now.AddMinutes(30),
                            ConfirmedChange  = false
                        };
                        var userManagerResult = await _userManagerRepository.AddAsync(userManagerResponse);
                        string confirmarEmail = String.Empty;

                        if (userManagerResult)
                        {
                            var userManager = await _userManagerRepository.GetFirstOrDefaultAsync(x =>
                                x.ManagerCode.Equals(userManagerResponse.ManagerCode) &&
                                x.EmailId.Equals((int)EmailEnum.ConfirmationEmail) &&
                                x.UserId.Equals(user.Id), o => o.ExpirationDate);

                            if (userManager == null)
                            {
                                _userValidator.Errors.Add("Algo deu errado, tente novamente ou entre em contato com o suporte.");
                                return _userValidator;
                            }

                            var sendEmailRequest = new SendEmailRequest
                            {
                                Id = userManager.EmailId,
                                UserManagerId = userManager.Id,
                                EmailTO = user.Email,
                                ActivationCode = userManager.ManagerCode
                            };

                            var emailResult = await _emailService.SendAsync(sendEmailRequest);
                            if (emailResult.IsValid)
                                confirmarEmail = emailResult.Message;
                            else
                                confirmarEmail = emailResult.Errors.FirstOrDefault();
                        }

                        var response = _mapper.Map<UserResponse>(user);

                        _userValidator.Data = response;
                        _userValidator.Message = $"Usuário {request.Username}, {request.Email} registrado com sucesso. {confirmarEmail}";
                    }
                    else
                        _userValidator.Errors.Add($"Erro ao registrar usuário.");
                }
            }
            catch (Exception ex)
            {
                _userValidator.Errors.Add($"Erro ao registrar usuário: {ex.Message}");
            }

            return _userValidator;
        }

        public async Task<UserValidator> EditAsync(EditUserRequest request)
        {
            var userAdmin = _mapper.Map<EditFullUserRequest>(request);
            return await EditAsync(userAdmin);
        }

        public async Task<UserValidator> EditAsync(EditFullUserRequest request)
        {
            try
            {
                _userValidator.ValidatesUserEdition(request);

                if (!_userValidator.IsValid)
                    return _userValidator;

                var compareUser = await _userRepository.GetAsync(x => x.Id != request.Id && x.Username.Equals(request.Username));
                if (compareUser.ToList().Count > 0)
                {
                    _userValidator.Errors.Add("Já existe usuário cadastrado com esse nome de usuário.");
                    return _userValidator;
                }

                compareUser = await _userRepository.GetAsync(x => x.Id != request.Id && x.Email.Equals(request.Email));
                if (compareUser.ToList().Count > 0)
                {
                    _userValidator.Errors.Add("Já existe usuário cadastrado com esse e-mail.");
                    return _userValidator;
                }

                compareUser = await _userRepository.GetAsync(x => x.Id.Equals(request.Id));
                if (compareUser.ToList().Count == 0)
                {
                    _userValidator.Errors.Add("Usuário não encontrado.");
                    return _userValidator;
                }

                if (!string.IsNullOrEmpty(request.OldPassword))
                {
                    var userFound = compareUser.FirstOrDefault();
                    var oldPasswordHash = Encrypt.Sha256encrypt(request.OldPassword);
                    var result = await _userRepository.LoginAsync(userFound.Email, oldPasswordHash);

                    if (result == null)
                        _userValidator.Errors.Add("E-mail ou Senha antiga incorreto.");
                    else if (String.IsNullOrEmpty(userFound.Password) || userFound.Password != oldPasswordHash)
                        _userValidator.Errors.Add("Senha antiga incorreta.");
                }

                var userMapper = _mapper.Map<User>(request);

                if (!string.IsNullOrEmpty(request.Password))
                    userMapper.Password = Encrypt.Sha256encrypt(request.Password);

                var userResult = await _userRepository.EditAsync(userMapper);
                if (userResult)
                {
                    var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email.Equals(userMapper.Email));
                    if (user != null)
                    {
                        var response = _mapper.Map<UserResponse>(user);
                        _userValidator.Data = response;
                    }

                    _userValidator.Message = $"Usuário {request.Username}, {request.Email} atualizado com sucesso.";

                    return _userValidator;
                }

                _userValidator.Errors.Add("Erro ao atualizar usuário.");
            }
            catch (Exception ex)
            {
                _userValidator.Errors.Add($"Erro ao atualizar usuário: {ex.Message}");
            }

            return _userValidator;
        }
    }
}
