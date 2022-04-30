using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Helper.Validation;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Validations
{
    public class UserValidation
    {
        public List<string> ValidaRegistroUsuario(AddUserRequest model)
        {
            var retorno = new List<string>();
            Validate validar = new Validate();

            if (!validar.ValidarEmail(model.Email))
                retorno.Add("Email inválido.");

            else if (model.Email != model.ReEmail)
                retorno.Add("Confirmação do e-mail não confere.");

            else if (model.Password != model.RePassword)
                retorno.Add("Confirmação da senha não confere.");

            else if (model.Password.Length < 8)
                retorno.Add("A senha deve conter no mínimo 8 caracteres.");

            return retorno;
        }

        public List<string> ValidaAlteracaoUsuario(EditUserRequest model)
        {
            var retorno = new List<string>();
            Validate validar = new Validate();

            if (!validar.ValidarEmail(model.Email))
                retorno.Add("Email inválido.");

            else if (model.Email != model.ReEmail)
                retorno.Add("Confirmação do e-mail não confere.");

            else if (!string.IsNullOrEmpty(model.Password))
            {
                if (model.Password != model.RePassword)
                    retorno.Add("Confirmação da senha não confere.");

                else if (model.Password.Length < 8)
                    retorno.Add("A senha deve conter no mínimo 8 caracteres.");
            }

            return retorno;
        }
    }
}
