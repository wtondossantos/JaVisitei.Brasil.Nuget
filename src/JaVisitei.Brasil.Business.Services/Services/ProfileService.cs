using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.Validations;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using JaVisitei.Brasil.Business.ViewModels.Response;
using JaVisitei.Brasil.Business.ViewModels.Response.Profile;
using JaVisitei.Brasil.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;

namespace JaVisitei.Brasil.Business.Service
{
    public class ProfileService : BaseService<User>, IProfileService
    {
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;

        public ProfileService(IUserRepository userRepository,
            IMapper mapper) : base(userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var validacao = new ValidationResponse();
            validacao.Message = new List<string>();

            try
            {
                var usuario = _mapper.Map<User>(request);
                var resultado = await _userRepository.LoginAsync(usuario);

                if (resultado != null && !String.IsNullOrEmpty(resultado.Password))
                {
                    var tokenizar = new TokenString();
                    var token = tokenizar.CreateToken(resultado);

                    validacao.Message.Add("Login realizado com sucesso.");
                    validacao.Successfully = true;
                    validacao.Code = 1;

                    return new LoginResponse
                    {
                        Id = resultado.Id,
                        Expiration = DateTime.Now.AddMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIDED_MINUTE"))),
                        Token = token,
                        Validation = validacao
                    };
                }

                validacao.Message.Add("Usuário ou senha inválido.");
            }
            catch (Exception ex)
            {
                validacao.Message.Add($"Exception: {ex.Message}");
                validacao.Code = -1;
            }

            return new LoginResponse { Validation = validacao };
        }
    }
}
