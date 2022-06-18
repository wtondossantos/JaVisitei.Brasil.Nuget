using JaVisitei.Brasil.Business.ViewModels.Response.Email;
using JaVisitei.Brasil.Business.ViewModels.Request.Email;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Helper.Formatting;
using JaVisitei.Brasil.Data.Entities;
using System.Threading.Tasks;
using System.Net.Mail;
using System;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class EmailService : Service<Email>, IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly EmailValidator _emailValidator;

        public EmailService(IEmailRepository emailRepository,
            EmailValidator emailValidator) : base(emailRepository) {
            _emailRepository = emailRepository;
            _emailValidator = emailValidator; 
        }

        public async Task<EmailValidator> SendEmailUserManagerAsync(string email, UserManager userManager)
        {
            return await SendEmailUserManagerAsync(new SendEmailUserManagerRequest
            {
                Id = userManager.EmailId,
                UserManagerId = userManager.Id,
                Email = email,
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

                var message = _emailRepository.MailMassageConfig(email);
                message.Body = AssembleBodyEmailUserManager(email, request);
                message.To.Add(new MailAddress(request.Email));
                message.CC.Add(new MailAddress("wellington@wton.com.br"));

                if (_emailRepository.Send(email.EmailConfig, message))
                {
                    _emailValidator.Data = new EmailUserManagerResponse
                    {
                        Sent = true,
                        Id = email.Id,
                        UserManagerId = request.UserManagerId
                    };
                    _emailValidator.Message = " E-mail enviado com sucesso!";
                }
                else
                    _emailValidator.Errors.Add("Erro ao tentar enviar mensagem de confirmação");
            }
            catch (Exception ex)
            {
                _emailValidator.Errors.Add($"Erro ao tentar enviar mensagem de confirmação. Detalhe do erro: {ex.Message}");
            }

            return _emailValidator; 
        }

        private static string AssembleBodyEmailUserManager(Email email, SendEmailUserManagerRequest request)
        {
            return email.Template.Template
                        .Replace("#message#", email.Message
                            .Replace("#codigoConfirmacao#", $"{request.ManagerCode}{request.UserManagerId}"))
                        .Replace("#header#", email.Header.Header)
                        .Replace("#footer#", email.Footer.Footer
                            .Replace("#codigoEmail#", Format.EmailTraceabilityCodeString(request.UserManagerId, email.Id)));
        }

        private static string AssembleBody(Email email)
        {
            return email.Template.Template
                        .Replace("#message#", email.Message)
                        .Replace("#header#", email.Header.Header)
                        .Replace("#footer#", email.Footer.Footer
                            .Replace("#codigoEmail#", Format.EmailTraceabilityCodeString(0, email.Id)));
        }
    }
}