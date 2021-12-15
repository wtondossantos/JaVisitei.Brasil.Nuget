using JaVisitei.Brasil.Business.ViewModels.Request;
using JaVisitei.Brasil.Helper;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Validations
{
    public class UsuarioValidation
    {
        public List<string> ValidaRegistroUsuario(UsuarioAdicionarRequest model)
        {
            var retorno = new List<string>();
            Validar validar = new Validar();

            if (!validar.ValidarEmail(model.Email))
                retorno.Add("Email inválido.");

            else if (model.Email != model.ConfirmarEmail)
                retorno.Add("Confirmação do e-mail não confere.");

            else if (model.Senha != model.ConfirmarSenha)
                retorno.Add("Confirmação da senha não confere.");

            else if (model.Senha.Length < 8)
                retorno.Add("A senha deve conter no mínimo 8 caracteres.");

            return retorno;
        }

        public List<string> ValidaAlteracaoUsuario(UsuarioAlterarRequest model, string email)
        {
            var retorno = new List<string>();
            Validar validar = new Validar();

            if (!validar.ValidarEmail(model.Email))
                retorno.Add("Email inválido.");

            else if (model.Email != email && model.Email != model.ConfirmarEmail)
                retorno.Add("Confirmação do e-mail não confere.");

            else if (!string.IsNullOrEmpty(model.Senha))
            {
                if (model.Senha != model.ConfirmarSenha)
                    retorno.Add("Confirmação da senha não confere.");

                else if (model.Senha.Length < 8)
                    retorno.Add("A senha deve conter no mínimo 8 caracteres.");
            }

            return retorno;
        }
    }
}
