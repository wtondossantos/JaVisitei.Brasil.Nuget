using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Helper.Validation;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Validations
{
    public class UserValidation
    {
        public List<string> ValidatesUserCreation(AddUserRequest request)
        {
            var reponse = new List<string>();
            Validate validate = new Validate();

            if (!validate.ValidateEmail(request.Email))
                reponse.Add("Email inválido.");

            else if (request.Email != request.ReEmail)
                reponse.Add("Confirmação do e-mail não confere.");

            else if (request.Password != request.RePassword)
                reponse.Add("Confirmação da senha não confere.");

            else if (request.Password.Length < 8)
                reponse.Add("A senha deve conter no mínimo 8 caracteres.");

            return reponse;
        }

        public List<string> ValidatesUserEdition(EditUserRequest request)
        {
            var response = new List<string>();
            Validate validate = new Validate();

            if (!validate.ValidateEmail(request.Email))
                response.Add("Email inválido.");

            else if (request.Email != request.ReEmail)
                response.Add("Confirmação do e-mail não confere.");

            else if (!string.IsNullOrEmpty(request.Password))
            {
                if (request.Password != request.RePassword)
                    response.Add("Confirmação da senha não confere.");

                else if (request.Password.Length < 8)
                    response.Add("A senha deve conter no mínimo 8 caracteres.");
            }

            return response;
        }
    }
}
