using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.Validation.Expressions;
using JaVisitei.Brasil.Business.Validation.Models;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;

namespace JaVisitei.Brasil.Business.Validation.Validators
{
    public class ProfileValidator<T> : ModelValidator<T>
    {
        public ProfileValidator() { }

        public void ValidatesLogin(LoginRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(request.Email))
                Errors.Add("Informe o e-mail ou usuário.");

            if (string.IsNullOrEmpty(request.Password))
                Errors.Add("Informe a senha.");
        }

        public void ValidatesRefleshToken(RefreshTokenRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            ValidatesEmail(request.Email);

            if (string.IsNullOrEmpty(request.RToken))
                Errors.Add("Informe o RToken");

            if (string.IsNullOrEmpty(request.RefreshToken))
                Errors.Add("Informe o RefreshToken");
        }

        public void ValidatesConfirmationEmail(ActiveAccountRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(request.ActivationCode))
                Errors.Add("Informe o código de ativação");

            else if (!ManagerCodeRegex.ValidateManagerCodeFull(request.ActivationCode))
                Errors.Add("Informe o código de ativação correto.");
        }

        public void ValidatesEmail(GenerateConfirmationCodeRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            ValidatesEmail(request.Email);
        }

        public void ValidatesEmail(ForgotPasswordRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            ValidatesEmail(request.Email);
        }

        public void ValidatesResetPassword(ResetPasswordRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            ValidatesEmail(request.Email);

            if (string.IsNullOrEmpty(request.ResetPasswordCode))
                Errors.Add("Informe o código enviado por e-mail.");

            else if (!ManagerCodeRegex.ValidateManagerCodeFull(request.ResetPasswordCode))
                Errors.Add("Código informado inválido.");

            if (string.IsNullOrEmpty(request.Password) || request.Password != request.RePassword)
                Errors.Add("Confirmação da senha não confere.");

            else if (!PasswordRegex.ValidatePassword(request.Password))
                Errors.Add("A senha deve conter no mínimo 8 caracteres, com pelo menos uma letra mínúscula, uma maiúscola e um número.");
        }

        public void ValidatesEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                Errors.Add("Informe o e-mail.");

            else if (!EmailRegex.ValidateEmail(email))
                Errors.Add("Email inválido.");
        }

        public void ValidatesEmailConfirmationCodeExpirationTime(DateTime expirationDate)
        {
            if (expirationDate < DateTime.Now)
                Errors.Add("Cógido de confirmação de conta expirado, solicite um novo código.");
        }

        public void ValidatesPasswordConfirmationCodeExpirationTime(DateTime expirationDate)
        {
            if (expirationDate < DateTime.Now)
                Errors.Add("Cógido de troca de senha expirado, solicite um novo código.");
        }
    }
}
