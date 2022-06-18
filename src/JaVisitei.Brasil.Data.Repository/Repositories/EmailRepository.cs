using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System;
using System.Text;

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

        public MailMessage MailMassageConfig(Email email)
        {
            return new MailMessage
            {
                Subject = email.Subject,
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Priority = MailPriority.High,
                From = new MailAddress(email.EmailConfig.FromSmtp, email.EmailConfig.Name, Encoding.UTF8)
            };
        }

        public bool Send(EmailConfig config, MailMessage message)
        {
            try
            {
                var client = new SmtpClient
                {
                    Port = config.PortSmtp,
                    Host = config.ServerSmtp,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(config.FromSmtp, Environment.GetEnvironmentVariable("PASS_SMTP")),
                };

                client.Send(message);

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
