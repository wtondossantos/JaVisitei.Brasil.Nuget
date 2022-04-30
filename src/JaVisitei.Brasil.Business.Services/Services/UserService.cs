using JaVisitei.Brasil.Business.ViewModels.Response.Profile;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using JaVisitei.Brasil.Business.ViewModels.Response.User;
using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.ViewModels.Response;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Business.Validations;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using JaVisitei.Brasil.Helper.Others;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System;

namespace JaVisitei.Brasil.Business.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository,
            IMapper mapper) : base(userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AddUserResponse> AddAsync(AddUserRequest request)
        {
            var validation = new UserValidation();
            var response = new AddUserResponse();
            response.Validation = new ValidationResponse();
            response.Validation.Message = new List<string>();

            try
            {
                response.Validation.Message = validation.ValidatesUserCreation(request);
                if (response.Validation.Message.Count > 0)
                    return response;

                var userExists = await _userRepository.GetAsync(x => x.Email == request.Email || x.Username == request.Username);
                if (userExists.ToList().Count > 0)
                    response.Validation.Message.Add("Já existe usuário com este e-mail e/ou usuário.");
                else
                {
                    var user = _mapper.Map<User>(request);
                    var status = await _userRepository.AddAsync(user);

                    if (status == 1)
                    {
                        response.Validation.Successfully = true;
                        response.Validation.Message.Add($"Usuário {request.Username}, {request.Email} registrado com sucesso.");
                    }
                    else
                        response.Validation.Message.Add($"Erro ao registrar usuário.");

                    response.Validation.Code = status;
                }
            }
            catch (Exception ex)
            {
                response.Validation.Code = -1;
                response.Validation.Message.Add($"Erro ao registrar usuário: {ex.Message}");
            }

            return response;
        }

        public async Task<EditUserResponse> EditAsync(EditUserRequest request)
        {
            var validation = new UserValidation();
            var response = new EditUserResponse();
            response.Validation = new ValidationResponse();
            response.Validation.Message = new List<string>();
            
            try
            {
                response.Validation.Message = validation.ValidatesUserEdition(request);
                if (response.Validation.Message.Count > 0)
                    return response;

                var compareUser = await _userRepository.GetAsync(x => x.Id != request.Id && x.Username == request.Username);
                if (compareUser.ToList().Count > 0)
                {
                    response.Validation.Message.Add("Já existe usuário cadastrado com esse nome de usuário.");
                    return response;
                }

                compareUser = await _userRepository.GetAsync(x => x.Id != request.Id && x.Email == request.Email);
                if (compareUser.ToList().Count > 0)
                {
                    response.Validation.Message.Add("Já existe usuário cadastrado com esse e-mail.");
                    return response;
                }

                compareUser = await _userRepository.GetAsync(x => x.Id == request.Id);
                if (compareUser == null)
                {
                    response.Validation.Message.Add("Usuário não encontrado.");
                    return response;
                }

                if (!string.IsNullOrEmpty(request.OldPassword))
                {
                    var userFound = compareUser.FirstOrDefault();
                    var oldPasswordHash = Encrypt.Sha256encrypt(request.OldPassword);
                    var result = await _userRepository.LoginAsync(new User()
                    {
                        Email = userFound.Email,
                        Password = oldPasswordHash
                    });

                    if (result == null)
                        response.Validation.Message.Add("E-mail ou Senha antiga incorreto.");

                    else if (String.IsNullOrEmpty(userFound.Password) || userFound.Password != oldPasswordHash)
                        response.Validation.Message.Add("Senha antiga incorreta.");
                }

                var user = _mapper.Map<User>(request);

                if (!string.IsNullOrEmpty(request.Password))
                    user.Password = Encrypt.Sha256encrypt(request.Password);

                var status = await _userRepository.EditAsync(user);

                if (status == 1)
                {
                    response.Validation.Successfully = true;
                    response.Validation.Message.Add($"Usuário {request.Username}, {request.Email} atualizado com sucesso.");
                }
                else
                    response.Validation.Message.Add($"Erro ao atualizar usuário.");

                response.Validation.Code = status;
            }
            catch (Exception ex)
            {
                response.Validation.Message.Add($"Erro ao atualizar usuário: {ex.Message}");
                response.Validation.Code = -1;
            }

            return response;
        }
    }
}
