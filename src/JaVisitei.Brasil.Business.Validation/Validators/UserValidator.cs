using JaVisitei.Brasil.Business.Validation.Expressions;
using JaVisitei.Brasil.Business.Validation.Models;
using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.ViewModels.Response.User;

namespace JaVisitei.Brasil.Business.Validation.Validators
{
    public class UserValidator : ModelValidator<UserResponse>
    {
        public UserValidator() { }

        public void ValidatesUserCreation(AddFullUserRequest request)
        {
            ValidatesUserName(request.Name, request.Surname);
            ValidatesUserUsername(request.Username);
            ValidatesUserEmail(request.Email, request.ReEmail);
            ValidatesUserPassword(request.Password, request.RePassword);
        }

        public void ValidatesUserEdition(EditFullUserRequest request)
        {
            ValidatesUserName(request.Name, request.Surname);
            ValidatesUserUsername(request.Username);
            ValidatesUserEmail(request.Email, request.ReEmail);

            if (!string.IsNullOrEmpty(request.Password))
            {
                if (String.IsNullOrEmpty(request.OldPassword))
                    Errors.Add("Informe a senha antiga.");
                
                ValidatesUserPassword(request.Password, request.RePassword);
            }
        }

        public void ValidatesUserName(string name, string surname)
        {
            if (name.Length < 3 || name.Length > 50)
                Errors.Add("Informe seu nome corretamente.");

            if(!String.IsNullOrEmpty(surname) && surname.Length > 200)
                Errors.Add("Não é suportado sobrenome com mais de 200 caracter.");

            if (!OnlyTextRegex.ValidateOnlyText(name))
                Errors.Add("O Nome possui caracter inválido. Permitido somente letras.");

            if (!OnlyTextRegex.ValidateOnlyText(surname))
                Errors.Add("O Sobrenome possui caracter inválido. Permitido somente letras.");
        }

        public void ValidatesUserUsername(string username)
        {
            if (username.Length < 3)
                Errors.Add("Informe o Username com mais de 3 caracteres.");

            else if (username.Length > 50)
                Errors.Add("Informe o Username com menos de 50 caracteres.");

            else if (!UsernameRegex.ValidateUsername(username))
                Errors.Add("Informe o nome de usuário válido, sem espaço. Caracter especial permitido: (_) Underline.");
        }

        public void ValidatesUserEmail(string email, string reEmail)
        {
            if (email != reEmail)
                Errors.Add("Confirmação do e-mail não confere.");

            else if (!EmailRegex.ValidateEmail(email.ToLower()))
                Errors.Add("Email inválido.");
        }

        public void ValidatesUserPassword(string password, string rePassword)
        {
            if (password != rePassword)
                Errors.Add("Confirmação da senha não confere.");

            else if (!PasswordRegex.ValidatePassword(password))
                Errors.Add("A senha deve conter no mínimo 8 caracteres, com pelo menos uma letra mínúscula, uma maiúscola e um número.");
        }
    }
}
