using JaVisitei.Brasil.Business.Validation.Expressions;
using JaVisitei.Brasil.Business.Validation.Models;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;

namespace JaVisitei.Brasil.Business.Validation.Validators
{
    public class ProfileValidator<T> : ModelValidator<T>
    {
        public ProfileValidator() { }

        public void ValidatesConfirmationEmail(string activationCode)
        {
            if (String.IsNullOrEmpty(activationCode))
                Errors.Add("Informe o código de ativação");

            else if (!AlphanumericCodeRegex.ValidateAlphanumericCode(activationCode))
                Errors.Add("Informe o código de ativação correto.");
        }

        public void ValidatesExpirationDate(DateTime expirationDate)
        {
            if (expirationDate < DateTime.Now)
                Errors.Add("Cógido expirado, solicite um novo código.");
        }

        public void ValidatesEmail(string email)
        {
           if (!EmailRegex.ValidateEmail(email.ToLower()))
                Errors.Add("Email inválido.");
        }

        public void ValidatesResetPassword(ResetPasswordRequest request)
        {
            ValidatesEmail(request.Email);

            if (String.IsNullOrEmpty(request.ResetPasswordCode))
                Errors.Add("Informe o código enviado por e-mail.");

            else if (!AlphanumericCodeRegex.ValidateAlphanumericCode(request.ResetPasswordCode))
                Errors.Add("Código informado inválido.");

            if (request.Password != request.RePassword)
                Errors.Add("Confirmação da senha não confere.");

            else if (!PasswordRegex.ValidatePassword(request.Password))
                Errors.Add("A senha deve conter no mínimo 8 caracteres, com pelo menos uma letra mínúscula, uma maiúscola e um número.");
        }
    }
}
