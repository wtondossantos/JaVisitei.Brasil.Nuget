using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.Email;
using JaVisitei.Brasil.Business.ViewModels.Response.Email;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace JaVisitei.Brasil.Business.Service.Test.Mocks
{
    public static class EmailMock
    {
        public static SendEmailUserManagerRequest ReturnSendEmailRequestMock()
        {
            return new SendEmailUserManagerRequest
            {
                Id = 1,
                UserManagerId = 1,
                Email = "teste@teste.com.zz",
                ManagerCode = "ABC123AB00"
            };
        }

        public static SendEmailUserManagerRequest ReturnSendEmailRequestInvalidMock()
        {
            return new SendEmailUserManagerRequest
            {
                Id = 0,
                UserManagerId = 0,
                Email = "testetestecomzz",
                ManagerCode = "#$"
            };
        }

        public static EmailValidator ReturnEmailUserManagerResponseMock()
        {
            return new EmailValidator
            {
                Data = new EmailUserManagerResponse
                {
                    Id = 1,
                    Sent = true,
                    UserManagerId = 1
                },
                Errors = new List<string>(),
                Message = "Sucess"
            };
        }

        public static EmailValidator ReturnEmailUserManagerInvalidMock()
        {
            return new EmailValidator
            {
                Errors = new List<string>{
                    "Invalid return"
                }
            };
        }

        public static EmailValidator ReturnEmailUserManagerErroMock()
        {
            return new EmailValidator
            {
                Errors = new List<string>()
            };
        }
        
        public static Email ReturnEmailMock()
        {
            return new Email
            {
                Id=1,
                HeaderId = 1,
                FooterId = 1,
                TemplateId = 1,
                EmailConfigId = 1,
                Message = "Body e-mail",
                Subject = "Subject E-mail",
                EmailConfig = EmailConfigMock(),
                Footer = EmailFooterMock(),
                Header = EmailHeaderMock(),
                Template = EmailTemplateMock(),
                UserManagers = new List<UserManager>()
            };
        }

        public static MailMessage MailMassageConfigMock()
        {
            return new MailMessage
            {
                Subject = "Subject E-mail",
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Priority = MailPriority.High,
                From = new MailAddress("teste@teste.com.zz", "Já Visitei", Encoding.UTF8)
            };
        }

        public static EmailTemplate EmailTemplateMock()
        {
            return new EmailTemplate
            {
                Template = "Template e-mail"
            };
        }

        public static EmailHeader EmailHeaderMock()
        {
            return new EmailHeader
            {
                Header = "Header e-mail"
            };
        }

        public static EmailFooter EmailFooterMock()
        {
            return new EmailFooter
            {
                Footer = "Footer e-mail"
            };
        }

        public static EmailConfig EmailConfigMock()
        {
            return new EmailConfig
            {
                Id = 1,
                ServerSmtp = "smtp.mail.com",
                FromSmtp = "smtp@teste.com.zz",
                PortSmtp = 123,
                Name = "Já Visitei",
                Emails = new List<Email>()
            };
        }
    }
}
