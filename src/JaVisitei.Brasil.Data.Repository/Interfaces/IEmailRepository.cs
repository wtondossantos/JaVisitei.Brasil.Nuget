using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using MimeKit;
using System.Net.Mail;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IEmailRepository : IRepository<Email>
    {
        Task<Email> GetFullByIdAsync(int emailId);
        MimeMessage AssembleMessage(Email config, string body, string emailTo, string emailCC = null, string subject = "");
        Task<bool> Send(EmailConfig config, MimeMessage message);
    }
}
