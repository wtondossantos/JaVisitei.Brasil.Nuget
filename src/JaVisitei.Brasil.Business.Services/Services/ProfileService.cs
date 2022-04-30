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
            var validation = new ValidationResponse();
            validation.Message = new List<string>();

            try
            {
                var usuario = _mapper.Map<User>(request);
                var result = await _userRepository.LoginAsync(usuario);

                if (result != null && !String.IsNullOrEmpty(result.Password))
                {
                    var tokenize = new TokenString();
                    var token = tokenize.CreateToken(result);

                    validation.Message.Add("Login realizado com sucesso.");
                    validation.Successfully = true;
                    validation.Code = 1;

                    return new LoginResponse
                    {
                        Id = result.Id,
                        Expiration = DateTime.Now.AddMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIDED_MINUTE"))),
                        Token = token,
                        Validation = validation
                    };
                }

                validation.Message.Add("Usuário ou senha inválido.");
            }
            catch (Exception ex)
            {
                validation.Message.Add($"Exception: {ex.Message}");
                validation.Code = -1;
            }

            return new LoginResponse { Validation = validation };
        }
    }
}
