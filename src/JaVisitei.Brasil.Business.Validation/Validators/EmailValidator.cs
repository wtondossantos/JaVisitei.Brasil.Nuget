using JaVisitei.Brasil.Business.Validation.Expressions;
using JaVisitei.Brasil.Business.Validation.Models;
using JaVisitei.Brasil.Business.ViewModels.Request.Email;
using JaVisitei.Brasil.Business.ViewModels.Response.Email;

namespace JaVisitei.Brasil.Business.Validation.Validators
{
    public class EmailValidator : ModelValidator<EmailUserManagerResponse>
    {
        public EmailValidator() { }

        public void ValidatesSendingConfirmationEmailUserManager(SendEmailUserManagerRequest request)
        {
            if(request is null)
                throw new ArgumentNullException(nameof(request));

            if (request.Id.Equals(0))
                Errors.Add("Informe o código da mensagem.");

            if (request.UserManagerId.Equals(0))
                Errors.Add("Informe o código do usuário confirmação.");

            if (!EmailRegex.ValidateEmail(request.Email))
                Errors.Add("E-mail informado inválido.");

            if(!ManagerCodeRegex.ValidateManagerCode(request.ManagerCode))
                Errors.Add("Código de ativação inválido.");
        }
    }
}