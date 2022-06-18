using JaVisitei.Brasil.Business.Validation.Expressions;
using JaVisitei.Brasil.Business.Validation.Models;
using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.ViewModels.Response.User;

namespace JaVisitei.Brasil.Business.Validation.Validators
{
    public class UserValidator : ModelValidator<UserResponse>
    {
        public UserValidator() { }

        public void ValidatesUserCreation(InsertFullUserRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            ValidatesUserName(request.Name);
            ValidatesUserSurname(request.Surname);
            ValidatesUserUsername(request.Username);
            ValidatesUserEmail(request.Email, request.ReEmail);
            ValidatesUserPassword(request.Password, request.RePassword);
        }

        public void ValidatesUserEdition(UpdateFullUserRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            ValidatesUserName(request.Name);
            ValidatesUserSurname(request.Surname);
            ValidatesUserUsername(request.Username);
            ValidatesUserEmail(request.Email, request.ReEmail);

            if (!string.IsNullOrEmpty(request.Password))
            {
                if (string.IsNullOrEmpty(request.OldPassword))
                    Errors.Add("Informe a senha antiga.");
                
                ValidatesUserPassword(request.Password, request.RePassword);
            }
        }

        public void ValidatesUserName(string name)
        {
            if (string.IsNullOrEmpty(name))
                Errors.Add("Informe seu nome corretamente.");

            else if (name.Length < 3 || name.Length > 50)
                Errors.Add("Informe seu nome corretamente, mínimo 3 e máximo 50 caracteres.");

            else if (!OnlyTextRegex.ValidateOnlyText(name))
                Errors.Add("O Nome possui caracter inválido. Permitido somente letras.");
        }

        public void ValidatesUserSurname(string surname)
        {
            if (!string.IsNullOrEmpty(surname) && surname.Length > 200)
                Errors.Add("Não é suportado sobrenome com mais de 200 caracter.");

            else if (!string.IsNullOrEmpty(surname) && !OnlyTextRegex.ValidateOnlyText(surname))
                Errors.Add("O Sobrenome possui caracter inválido. Permitido somente letras.");
        }

        public void ValidatesUserUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                Errors.Add("Informe o Username.");

            else if (username.Length < 3)
                Errors.Add("Informe o Username com mais de 3 caracteres.");

            else if (username.Length > 50)
                Errors.Add("Informe o Username com menos de 50 caracteres.");

            else if (!UsernameRegex.ValidateUsername(username))
                Errors.Add("Informe o nome de usuário válido, sem espaço. Caracter especial permitido: (_) Underline.");
        }

        public void ValidatesUserEmail(string email, string reEmail)
        {
            if (string.IsNullOrEmpty(email) || email.Length < 5)
                Errors.Add("Informe um e-amil válido");

            else if (email.Length > 200)
                Errors.Add("Informe um e-amil válido, o e-mail não pode ser maior que 200 caracteres.");

            else if (email != reEmail)
                Errors.Add("Confirmação do e-mail não confere.");

            else if (!EmailRegex.ValidateEmail(email))
                Errors.Add("Email inválido.");
        }

        public void ValidatesUserPassword(string password, string rePassword)
        {
            if (string.IsNullOrEmpty(password) || password != rePassword)
                Errors.Add("Confirmação da senha não confere.");

            else if (!PasswordRegex.ValidatePassword(password))
                Errors.Add("A senha deve conter no mínimo 8 caracteres e no máximo 50, com pelo menos uma letra minúscula, uma maiúscula e um número.");
        }
    }
}
