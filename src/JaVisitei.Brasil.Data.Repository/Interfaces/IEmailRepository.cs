using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Net.Mail;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IEmailRepository : IRepository<Email>
    {
        Task<Email> GetFullByIdAsync(int emailId);
        bool Send(EmailConfig config, MailMessage message);
        MailMessage MailMassageConfig(Email email);
    }
}
