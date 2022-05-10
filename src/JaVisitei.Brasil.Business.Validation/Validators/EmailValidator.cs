using JaVisitei.Brasil.Business.Validation.Expressions;
using JaVisitei.Brasil.Business.Validation.Models;
using JaVisitei.Brasil.Business.ViewModels.Request.Email;
using JaVisitei.Brasil.Business.ViewModels.Response.Email;

namespace JaVisitei.Brasil.Business.Validation.Validators
{
    public class EmailValidator : ModelValidator<EmailResponse>
    {
        public EmailValidator() { }

        public void ValidatesSendingConfirmationEmail(SendEmailRequest request)
        {
            if (request.Id.Equals(0))
                Errors.Add("Informe o código da mensagem.");

            if (request.UserManagerId.Equals(0))
                Errors.Add("Informe o código do usuário confirmação.");

            else if (!EmailRegex.ValidateEmail(request.EmailTO.ToLower()))
                Errors.Add("E-mail informado inválido.");

            if (String.IsNullOrEmpty(request.ActivationCode))
                Errors.Add("Informe o código de ativação");
        }
    }
}