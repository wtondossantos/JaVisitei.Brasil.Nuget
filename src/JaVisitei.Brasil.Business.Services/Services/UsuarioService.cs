using JaVisitei.Brasil.Business.ViewModels.Response;
using JaVisitei.Brasil.Business.ViewModels.Request;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using JaVisitei.Brasil.Security;
using System.Threading.Tasks;
using AutoMapper;
using System;

namespace JaVisitei.Brasil.Business.Service
{
    public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private IMapper _mapper;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper) : base(repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AutenticacaoResponse> AutenticacaoAsync(AutenticacaoRequest autenticacaoRequest)
        {
            var validacao = new ValidacaoResponse();
            validacao.Mensagem = new List<string>();

            try
            {
                var usuario = _mapper.Map<Usuario>(autenticacaoRequest);
                var resultado = await _repository.AutenticacaoAsync(usuario);

                if (resultado != null && !String.IsNullOrEmpty(resultado.Senha))
                {
                    var tokenizar = new TokenString(resultado);
                    var token = tokenizar.GerarToken();

                    validacao.Mensagem.Add("Login realizado com sucesso.");
                    validacao.Sucesso = true;
                    validacao.Codigo = 1;

                    return new AutenticacaoResponse
                    {
                        Expira = DateTime.Now.AddMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIDED_MINUTE"))),
                        Token = token,
                        Validacao = validacao
                    };
                }

                validacao.Mensagem.Add("Usuário ou senha inválido.");
            }
            catch (Exception ex)
            {
                validacao.Mensagem.Add($"Exception: {ex.Message}");
                validacao.Codigo = -1;
            }

            return new AutenticacaoResponse { Validacao = validacao };
        }
    }
}
