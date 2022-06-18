using JaVisitei.Brasil.Business.ViewModels.Request.Email;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.Service.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Services;
using JaVisitei.Brasil.Data.Entities;
using System.Threading.Tasks;
using System;
using Moq;

namespace JaVisitei.Brasil.Business.Service.Test.Services
{
    [TestClass]
    public class EmailServiceTest
    {
        private EmailService _emailService;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<IEmailRepository> _mockEmailRepository;

        public EmailServiceTest()
        {
            _mockEmailService = new Mock<IEmailService>();
            _mockEmailRepository = new Mock<IEmailRepository>();
            _emailService = new EmailService(_mockEmailRepository.Object, new EmailValidator());
        }

        #region Send email user manager 

        [TestMethod("Send email user manager Instance Correct return")]
        public async Task SendEmailUserManagerAsync_ShouldCorrectReturn_SendEmailUserManager()
        {
            var email = "teste@teste.com.zz";
            var userManager = UserManagerMock.ReturnUserManagerNotConfirmedMock();
            var emailValidation = EmailMock.ReturnEmailUserManagerResponseMock();

            _ = _mockEmailService
                .Setup(x => x.SendEmailUserManagerAsync(It.IsAny<SendEmailUserManagerRequest>()))
                .ReturnsAsync(emailValidation);

            var result = await _emailService.SendEmailUserManagerAsync(email, userManager);

            Assert.IsNotNull(result);
        }

        [TestMethod("Send email user manager Correct return")]
        public async Task SendEmailUserManagerAsync_ShouldCorretReturn_SendEmailRequest()
        {
            var sendEmailRequest = EmailMock.ReturnSendEmailRequestMock();
            var emailValidation = EmailMock.ReturnEmailUserManagerResponseMock();
            var emailResponse = EmailMock.ReturnEmailMock();
            var message = EmailMock.MailMassageConfigMock();
            _emailService = new EmailService(_mockEmailRepository.Object, emailValidation);

            _ = _mockEmailRepository
                .Setup(x => x.GetFullByIdAsync(sendEmailRequest.Id))
                .ReturnsAsync(emailResponse);

            _ = _mockEmailRepository
                .Setup(x => x.MailMassageConfig(emailResponse))
                .Returns(message);

            _ = _mockEmailRepository
                .Setup(x => x.Send(emailResponse.EmailConfig, message))
                .Returns(true);

            var result = await _emailService.SendEmailUserManagerAsync(sendEmailRequest);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(emailValidation.Data.Id, result.Data.Id);
            Assert.AreEqual(emailValidation.Data.Sent, result.Data.Sent);
            Assert.AreEqual(emailValidation.Data.UserManagerId, result.Data.UserManagerId);
        }

        [TestMethod("Send email user manager Invalid validation")]
        public async Task SendEmailUserManagerAsync_ShouldInvalidReturn_Validation()
        {
            var sendEmailRequest = EmailMock.ReturnSendEmailRequestInvalidMock();
            var emailValidation = EmailMock.ReturnEmailUserManagerInvalidMock();
            _emailService = new EmailService(_mockEmailRepository.Object, emailValidation);

            var result = await _emailService.SendEmailUserManagerAsync(sendEmailRequest);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Send email user manager Invalid return e-mail")]
        public async Task SendEmailUserManagerAsync_ShouldInvalidReturn_InvalidEmail()
        {
            var sendEmailRequest = EmailMock.ReturnSendEmailRequestMock();
            var emailValidation = EmailMock.ReturnEmailUserManagerErroMock();
            var message = EmailMock.MailMassageConfigMock();
            _emailService = new EmailService(_mockEmailRepository.Object, emailValidation);

            _ = _mockEmailRepository
                .Setup(x => x.GetFullByIdAsync(sendEmailRequest.Id))
                .ReturnsAsync((Email)null);

            var result = await _emailService.SendEmailUserManagerAsync(sendEmailRequest);
            
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Send email user manager False return")]
        public async Task SendEmailUserManagerAsync_ShouldFalseReturn_SendEmailFalse()
        {
            var sendEmailRequest = EmailMock.ReturnSendEmailRequestMock();
            var emailValidation = EmailMock.ReturnEmailUserManagerErroMock();
            var emailResponse = EmailMock.ReturnEmailMock();
            var message = EmailMock.MailMassageConfigMock();
            _emailService = new EmailService(_mockEmailRepository.Object, emailValidation);

            _ = _mockEmailRepository
                .Setup(x => x.GetFullByIdAsync(sendEmailRequest.Id))
                .ReturnsAsync(emailResponse);

            _ = _mockEmailRepository
                .Setup(x => x.MailMassageConfig(emailResponse))
                .Returns(message);

            _ = _mockEmailRepository
                .Setup(x => x.Send(emailResponse.EmailConfig, message))
                .Returns(false);

            var result = await _emailService.SendEmailUserManagerAsync(sendEmailRequest);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Send email user manager Return e-mail exception")]
        public async Task SendEmailUserManagerAsync_ShouldProbrem_EmailException()
        {
            var sendEmailRequest = EmailMock.ReturnSendEmailRequestMock();
            var message = "Exception test";

            _ = _mockEmailRepository
                .Setup(x => x.GetFullByIdAsync(sendEmailRequest.Id))
                .Throws(new Exception(message));

            var result = await _emailService.SendEmailUserManagerAsync(sendEmailRequest);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors[0].Contains(message));
        }


        [TestMethod("Send email user manager Return send exception")]
        public async Task SendEmailUserManagerAsync_ShouldProbrem_SendException()
        {
            var sendEmailRequest = EmailMock.ReturnSendEmailRequestMock();
            var emailValidation = EmailMock.ReturnEmailUserManagerErroMock();
            var emailResponse = EmailMock.ReturnEmailMock();
            var message = EmailMock.MailMassageConfigMock();
            var exception = "Exception test";
            _emailService = new EmailService(_mockEmailRepository.Object, emailValidation);

            _ = _mockEmailRepository
                .Setup(x => x.GetFullByIdAsync(sendEmailRequest.Id))
                .ReturnsAsync(emailResponse);

            _ = _mockEmailRepository
                .Setup(x => x.MailMassageConfig(emailResponse))
                .Returns(message);

            _ = _mockEmailRepository
                .Setup(x => x.Send(emailResponse.EmailConfig, message))
                .Throws(new Exception(exception));

            var result = await _emailService.SendEmailUserManagerAsync(sendEmailRequest);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors[0].Contains(exception));
        }

        #endregion
    }
}
