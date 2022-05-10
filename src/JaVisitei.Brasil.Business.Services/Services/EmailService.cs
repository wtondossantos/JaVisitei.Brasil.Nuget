using JaVisitei.Brasil.Business.ViewModels.Request.Email;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text;
using System;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class EmailService : BaseService<Email>, IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly EmailValidator _emailValidator;

        public EmailService(IEmailRepository emailRepository,
            EmailValidator emailValidator) : base(emailRepository) {
            _emailRepository = emailRepository;
            _emailValidator = emailValidator; 
        }

        public async Task<EmailValidator> SendAsync(SendEmailRequest request)
        {
            try
            {
                _emailValidator.ValidatesSendingConfirmationEmail(request);

                if (!_emailValidator.IsValid)
                    return _emailValidator;

                var email = await _emailRepository.GetEmailFirstOrDefaultAsync(request.Id);

                if (email == null)
                {
                    _emailValidator.Errors.Add("Mensagem de disparo não encontrada.");
                    return _emailValidator;
                }

                var message = new MailMessage
                {
                    Subject = email.Subject,
                    SubjectEncoding = Encoding.UTF8,
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    Body = AssembleBodyEmail(email, $"{request.ActivationCode}{request.UserManagerId}"),
                    From = new MailAddress(email.EmailConfig.FromSmtp, email.EmailConfig.Name, Encoding.UTF8)
                };
                message.To.Add(new MailAddress(request.EmailTO));
                message.CC.Add(new MailAddress("wellington@wton.com.br"));

                var result = _emailRepository.Send(email.EmailConfig, message);
                if (result)
                    _emailValidator.Message = " E-mail enviado com sucesso!";
                else
                    _emailValidator.Errors.Add("Erro ao tentar enviar mensagem de confirmação");
            }
            catch(Exception ex)
            {
                _emailValidator.Errors.Add($"Erro ao tentar enviar mensagem de confirmação. Detalhe do erro: {ex.Message}");
            }

            return _emailValidator; 
        }

        private static string AssembleBodyEmail(Email email, string code)
        {
            return email.Template.Template
                        .Replace("#message#", email.Message
                            .Replace("#codigoConfirmacao#", code))
                        .Replace("#header#", email.Header.Header)
                        .Replace("#footer#", email.Footer.Footer
                            .Replace("#codigoEmail#", email.Id.ToString())).ToString();
        }
    }
}