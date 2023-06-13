using JaVisitei.Brasil.Business.ViewModels.Response.Email;
using JaVisitei.Brasil.Business.ViewModels.Request.Email;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Helper.Formatting;
using JaVisitei.Brasil.Data.Entities;
using System.Threading.Tasks;
using JaVisitei.Brasil.Business.ViewModels.Request.Recaptcha;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class EmailService : Service<Email>, IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IRecaptchaService _recaptchaService;
        private readonly EmailValidator _emailValidator;

        public EmailService(IEmailRepository emailRepository,
            EmailValidator emailValidator,
            IRecaptchaService recaptchaService) : base(emailRepository) {
            _emailRepository = emailRepository;
            _emailValidator = emailValidator;
            _recaptchaService = recaptchaService;
        }

        public async Task<EmailValidator> SendEmailUserManagerAsync(UserManager userManager)
        {
            return await SendEmailUserManagerAsync(new SendEmailUserManagerRequest
            {
                Id = userManager.EmailId,
                UserManagerId = userManager.Id,
                Email = userManager.User.Email,
                UserName = userManager.User.Name,
                ManagerCode = userManager.ManagerCode
            });
        }

        public async Task<EmailValidator> SendEmailUserManagerAsync(SendEmailUserManagerRequest request)
        {
            try
            {
                _emailValidator.ValidatesSendingConfirmationEmailUserManager(request);

                if (!_emailValidator.IsValid)
                    return _emailValidator;

                var email = await _emailRepository.GetFullByIdAsync(request.Id);
                if (email is null)
                {
                    _emailValidator.Errors.Add("Mensagem para envio não encontrada.");
                    return _emailValidator;
                }

                var body = AssembleBodyEmailUserManager(email, request);
                var message = _emailRepository.AssembleMessage(email, body, request.Email);
                if (await _emailRepository.Send(email.EmailConfig, message))
                {
                    _emailValidator.Data = new EmailUserManagerResponse
                    {
                        Sent = true,
                        Id = email.Id,
                        UserManagerId = request.UserManagerId
                    };
                    _emailValidator.Message = " Código de configmração enviado para o e-mail de cadastro.";
                }
                else
                    _emailValidator.Errors.Add("Erro ao tentar enviar mensagem de confirmação.");
            }
            catch
            {
                _emailValidator.Errors.Add("Erro ao tentar enviar mensagem de confirmação.");
                throw;
            }

            return _emailValidator;
        }

        public async Task<EmailValidator> SendEmailContactAsync(SendEmailContactRequest request)
        {
            try
            {
                var validate = _recaptchaService.RetrieveAsync(new RecaptchaRequest { Key = request.Key, Response = request.Recaptcha });

                if (validate is null)
                    return null;

                if (!validate.Result.IsValid && validate.Result.Data is not null && !validate.Result.Data.Success)
                    return new EmailValidator { Errors = validate.Result.Errors };

                _emailValidator.ValidatesEmailContact(request);

                if (!_emailValidator.IsValid)
                    return _emailValidator;

                var email = await _emailRepository.GetFullByIdAsync(request.Id);
                if (email is null)
                {
                    _emailValidator.Errors.Add("Mensagem para envio não encontrada.");
                    return _emailValidator;
                }

                var body = AssembleBodyMessage(email, request);
                var message = _emailRepository.AssembleMessage(email, body, email.EmailConfig.Email, request.Email, request.Subject);
                if (await _emailRepository.Send(email.EmailConfig, message))
                {
                    _emailValidator.Data = new EmailUserManagerResponse
                    {
                        Sent = true,
                        Id = email.Id
                    };
                    _emailValidator.Message = $" Mensagem e cópia para {request.Email}, enviadas com sucesso.";
                }
                else
                    _emailValidator.Errors.Add("Erro ao tentar enviar mensagem de confirmação.");
            }
            catch
            {
                _emailValidator.Errors.Add("Erro ao tentar enviar mensagem de confirmação.");
                throw;
            }

            return _emailValidator;
        }

        private static string AssembleBodyEmailUserManager(Email email, SendEmailUserManagerRequest request)
        {
            return email.Template.Template
                        .Replace("#message#", email.Message
                            .Replace("#codigoConfirmacao#", $"{request.ManagerCode}{request.UserManagerId}")
                            .Replace("#nomeUsuario#", $"{request.UserName}"))
                        .Replace("#header#", email.Header.Header)
                        .Replace("#footer#", email.Footer.Footer
                            .Replace("#codigoEmail#", Format.EmailTraceabilityCodeString(request.UserManagerId, email.Id)));
        }

        private static string AssembleBodyMessage(Email email, SendEmailContactRequest request)
        {
            return email.Template.Template
                        .Replace("#message#", email.Message
                            .Replace("#subject#", request.Subject)
                            .Replace("#message#", request.Message))
                        .Replace("#header#", email.Header.Header)
                        .Replace("#footer#", email.Footer.Footer
                            .Replace("#codigoEmail#", Format.EmailTraceabilityCodeString(0, email.Id)));
        }
    }
}