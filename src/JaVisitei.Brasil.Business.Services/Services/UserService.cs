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
            var validacao = new UserValidation();
            var retorno = new AddUserResponse();
            retorno.Validation = new ValidationResponse();
            retorno.Validation.Message = new List<string>();

            try
            {
                retorno.Validation.Message = validacao.ValidaRegistroUsuario(request);
                if (retorno.Validation.Message != null && retorno.Validation.Message.Count > 0)
                    return retorno;

                var userExists = await _userRepository.GetAsync(x => x.Email == request.Email || x.Username == request.Username);
                if (userExists.ToList().Count > 0)
                    retorno.Validation.Message.Add("Já existe usuário com este e-mail e/ou usuário.");
                else
                {
                    var user = _mapper.Map<User>(request);
                    var status = await _userRepository.AddAsync(user);

                    if (status == 1)
                    {
                        retorno.Validation.Successfully = true;
                        retorno.Validation.Message.Add($"Usuário {request.Username}, {request.Email} registrado com sucesso.");
                    }
                    else
                        retorno.Validation.Message.Add($"Erro ao registrar usuário.");

                    retorno.Validation.Code = status;
                }
            }
            catch (Exception ex)
            {
                retorno.Validation.Code = -1;
                retorno.Validation.Message.Add($"Erro ao registrar usuário: {ex.Message}");
            }

            return retorno;
        }

        public async Task<EditUserResponse> EditAsync(EditUserRequest request)
        {
            var validacao = new UserValidation();
            var result = new EditUserResponse();
            result.Validation = new ValidationResponse();
            result.Validation.Message = new List<string>();
            
            try
            {
                result.Validation.Message = validacao.ValidaAlteracaoUsuario(request);
                if (result.Validation.Message != null && result.Validation.Message.Count > 0)
                    return result;

                var usuarioComparacao = await _userRepository.GetAsync(x => x.Id != request.Id && x.Username == request.Username);
                if (usuarioComparacao.ToList().Count > 0)
                {
                    result.Validation.Message.Add("Já existe usuário cadastrado com esse nome de usuário.");
                    return result;
                }

                usuarioComparacao = await _userRepository.GetAsync(x => x.Id != request.Id && x.Email == request.Email);
                if (usuarioComparacao.ToList().Count > 0)
                {
                    result.Validation.Message.Add("Já existe usuário cadastrado com esse e-mail.");
                    return result;
                }

                usuarioComparacao = await _userRepository.GetAsync(x => x.Id == request.Id);
                if (usuarioComparacao == null)
                {
                    result.Validation.Message.Add("Usuário não encontrado.");
                    return result;
                }

                if (!string.IsNullOrEmpty(request.OldPassword))
                {
                    var usuarioExistente = usuarioComparacao.FirstOrDefault();
                    var oldPasswordHash = Encrypt.Sha256encrypt(request.OldPassword);
                    var resultado = await _userRepository.LoginAsync(new User()
                    {
                        Email = usuarioExistente.Email,
                        Password = oldPasswordHash
                    });

                    if (resultado == null)
                        result.Validation.Message.Add("E-mail ou Senha antiga incorreto.");

                    else if (String.IsNullOrEmpty(usuarioExistente.Password) || usuarioExistente.Password != oldPasswordHash)
                        result.Validation.Message.Add("Senha antiga incorreta.");
                }

                var usuario = _mapper.Map<User>(request);

                if (!string.IsNullOrEmpty(request.Password))
                    usuario.Password = Encrypt.Sha256encrypt(request.Password);

                var status = await _userRepository.EditAsync(usuario);

                if (status == 1)
                {
                    result.Validation.Successfully = true;
                    result.Validation.Message.Add($"Usuário {request.Username}, {request.Email} atualizado com sucesso.");
                }
                else
                    result.Validation.Message.Add($"Erro ao atualizar usuário.");

                result.Validation.Code = status;
            }
            catch (Exception ex)
            {
                result.Validation.Message.Add($"Erro ao atualizar usuário: {ex.Message}");
                result.Validation.Code = -1;
            }

            return result;
        }
    }
}
