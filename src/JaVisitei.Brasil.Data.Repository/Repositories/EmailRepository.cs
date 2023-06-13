using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using MimeKit;
using MailKit.Net.Smtp;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class EmailRepository : Repository<Email>, IEmailRepository
    {
        public EmailRepository(DbJaVisiteiBrasilContext context) : base(context) { }

        public async Task<Email> GetFullByIdAsync(int emailId)
        {
            return await(from email in _context.Emails
                         join emailHeader in _context.EmailHeaders on email.HeaderId equals emailHeader.Id
                         join emailTemplate in _context.EmailTemplates on email.TemplateId equals emailTemplate.Id
                         join emailFooter in _context.EmailFooters on email.FooterId equals emailFooter.Id
                         join emailConfig in _context.EmailConfigs on email.EmailConfigId equals emailConfig.Id
                         where email.Id.Equals(emailId)
                         select new Email {
                             Id = email.Id, Message = email.Message, Subject = email.Subject,
                             HeaderId = email.HeaderId, FooterId = email.FooterId, 
                             TemplateId = email.TemplateId, EmailConfigId = email.EmailConfigId,
                             Header = emailHeader,
                             Footer = emailFooter,
                             Template = emailTemplate,
                             EmailConfig = emailConfig
                         }).FirstOrDefaultAsync();
        }

        public MimeMessage AssembleMessage(Email email, string body, string emailTo, string emailCC = null, string subject = "")
        {
            try
            {
                var messageBody = new BodyBuilder();
                messageBody.HtmlBody = body;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(email.EmailConfig.Name, email.EmailConfig.FromSmtp));
                message.Sender = new MailboxAddress(email.EmailConfig.Name, email.EmailConfig.FromSmtp);
                message.Body = messageBody.ToMessageBody();
                message.Subject = $"{email.Subject} {subject}";
                message.To.Add(MailboxAddress.Parse(emailTo));
                if(!string.IsNullOrEmpty(emailCC))
                    message.Cc.Add(MailboxAddress.Parse(emailCC));

                return message;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Send(EmailConfig config, MimeMessage message)
        {
            try
            {
                using var client = new SmtpClient();
                await client.ConnectAsync(config.ServerSmtp, config.PortSmtp, true);
                await client.AuthenticateAsync(config.FromSmtp, Environment.GetEnvironmentVariable("PASS_SMTP"));
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch
            {
                return false;
                throw;
            }
        }
    }
}
