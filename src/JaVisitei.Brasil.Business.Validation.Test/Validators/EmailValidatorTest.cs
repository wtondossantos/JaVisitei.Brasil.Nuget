using JaVisitei.Brasil.Business.Validation.Test.Mocks;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.Email;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Validation.Test.Validators
{
    [TestClass]
    public class EmailValidatorTest
    {
        private readonly EmailValidator _emailValidator;

        public EmailValidatorTest()
        {
            _emailValidator = new EmailValidator();
        }

        [TestMethod("E-mail validation Correct return")]
        public void ValidatesSendingConfirmationEmailUserManager_ShouldCorrectReturn_Validation()
        {
            var request = EmailMock.ReturnSendEmailRequestMock();

            _emailValidator.ValidatesSendingConfirmationEmailUserManager(request);

            Assert.IsTrue(_emailValidator.IsValid);
            Assert.IsTrue(_emailValidator.Errors.Count.Equals(0));
        }

        [TestMethod("E-mail validation Invalid return")]
        public void ValidatesSendingConfirmationEmailUserManager_ShouldInvalidReturn_Validation()
        {
            var request = EmailMock.ReturnSendEmailRequestInvalidMock();

            _emailValidator.ValidatesSendingConfirmationEmailUserManager(request);

            Assert.IsFalse(_emailValidator.IsValid);
            Assert.IsTrue(_emailValidator.Errors.Count.Equals(4));
        }

        [TestMethod("E-mail validation Invalid return empty")]
        public void ValidatesSendingConfirmationEmailUserManager_ShouldInvalidReturn_Empty()
        {
            var request = EmailMock.ReturnSendEmailRequestEmptyMock();

            _emailValidator.ValidatesSendingConfirmationEmailUserManager(request);

            Assert.IsFalse(_emailValidator.IsValid);
            Assert.IsTrue(_emailValidator.Errors.Count.Equals(4));
        }

        [TestMethod("E-mail validation Invalid return excpetion")]
        public void ValidatesSendingConfirmationEmailUserManager_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _emailValidator.ValidatesSendingConfirmationEmailUserManager(It.IsAny<SendEmailUserManagerRequest>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'request')");
        }
    }
}