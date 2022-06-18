using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.Validation.Test.Mocks;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using JaVisitei.Brasil.Business.ViewModels.Response.Profile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Validation.Test.Validators
{
    [TestClass]
    public class ProfileValidatorTest
    {
        private readonly ProfileValidator<LoginResponse> _profileLoginResponseValidator;
        private readonly ProfileValidator<ActivationResponse> _profileActivationResponseValidator;
        private readonly ProfileValidator<ForgotPasswordResponse> _profileForgotPasswordResponseValidator;
        private readonly ProfileValidator<ResetPasswordResponse> _profileResetPasswordValidator;
        private readonly ProfileValidator<GenerateConfirmationCodeResponse> _profileGenerateConfirmationCodeValidator;

        public ProfileValidatorTest()
        {
            _profileLoginResponseValidator = new ProfileValidator<LoginResponse>();
            _profileActivationResponseValidator = new ProfileValidator<ActivationResponse>();
            _profileForgotPasswordResponseValidator = new ProfileValidator<ForgotPasswordResponse>();
            _profileGenerateConfirmationCodeValidator = new ProfileValidator<GenerateConfirmationCodeResponse>();
            _profileResetPasswordValidator = new ProfileValidator<ResetPasswordResponse>();
        }

        #region Login

        [TestMethod("Login validation Correct return")]
        public void ValidatesSendingConfirmationEmailUserManager_ShouldCorrectReturn_Validation()
        {
            var request = ProfileMock.LoginRequestMock();

            _profileLoginResponseValidator.ValidatesLogin(request);

            Assert.IsTrue(_profileLoginResponseValidator.IsValid);
            Assert.IsTrue(_profileLoginResponseValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Login validation Invalid return")]
        public void ValidatesSendingConfirmationEmailUserManager_ShouldInvalidReturn_Validation()
        {
            var request = ProfileMock.LoginRequestInvalidMock();

            _profileLoginResponseValidator.ValidatesLogin(request);

            Assert.IsFalse(_profileLoginResponseValidator.IsValid);
            Assert.IsTrue(_profileLoginResponseValidator.Errors.Count.Equals(2));
        }

        [TestMethod("Login validation Invalid return empty")]
        public void ValidatesSendingConfirmationEmailUserManager_ShouldInvalidReturn_Empty()
        {
            var request = ProfileMock.LoginRequestEmptyMock();

            _profileLoginResponseValidator.ValidatesLogin(request);

            Assert.IsFalse(_profileLoginResponseValidator.IsValid);
            Assert.IsTrue(_profileLoginResponseValidator.Errors.Count.Equals(2));
        }

        [TestMethod("Login validation Invalid return nullable")]
        public void ValidatesSendingConfirmationEmailUserManager_ShouldInvalidReturn_Nulable()
        {
            _profileLoginResponseValidator.ValidatesLogin(new LoginRequest());

            Assert.IsFalse(_profileLoginResponseValidator.IsValid);
            Assert.IsTrue(_profileLoginResponseValidator.Errors.Count.Equals(2));
        }

        [TestMethod("Login validation Invalid return excpetion")]
        public void ValidatesSendingConfirmationEmailUserManager_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _profileLoginResponseValidator.ValidatesLogin(It.IsAny<LoginRequest>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'request')");
        }

        #endregion

        #region Active account

        [TestMethod("Active account validation Correct return")]
        public void ValidatesConfirmationEmail_ShouldCorrectReturn_Validation()
        {
            var request = ProfileMock.ActiveAccountRequestMock();

            _profileActivationResponseValidator.ValidatesConfirmationEmail(request);

            Assert.IsTrue(_profileActivationResponseValidator.IsValid);
            Assert.IsTrue(_profileActivationResponseValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Active account validation Invalid return")]
        public void ValidatesConfirmationEmail_ShouldInvalidReturn_Validation()
        {
            var request = ProfileMock.ActiveAccountRequestInvalidMock();

            _profileActivationResponseValidator.ValidatesConfirmationEmail(request);

            Assert.IsFalse(_profileActivationResponseValidator.IsValid);
            Assert.IsTrue(_profileActivationResponseValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Active account validation Invalid return empty")]
        public void ValidatesConfirmationEmail_ShouldInvalidReturn_Empty()
        {
            var request = ProfileMock.ActiveAccountRequestEmptyMock();

            _profileActivationResponseValidator.ValidatesConfirmationEmail(request);

            Assert.IsFalse(_profileActivationResponseValidator.IsValid);
            Assert.IsTrue(_profileActivationResponseValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Active account validation Invalid return nullable")]
        public void ValidatesConfirmationEmail_ShouldInvalidReturn_Nulable()
        {
            _profileActivationResponseValidator.ValidatesConfirmationEmail(new ActiveAccountRequest());

            Assert.IsFalse(_profileActivationResponseValidator.IsValid);
            Assert.IsTrue(_profileActivationResponseValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Active account validation Invalid return excpetion")]
        public void ValidatesConfirmationEmail_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _profileActivationResponseValidator.ValidatesConfirmationEmail(It.IsAny<ActiveAccountRequest>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'request')");
        }

        #endregion

        #region Forgot password

        [TestMethod("Forgot password validation Correct return")]
        public void ValidatesEmailForgotPassword_ShouldCorrectReturn_Validation()
        {
            var request = ProfileMock.ForgotPasswordRequestMock();

            _profileForgotPasswordResponseValidator.ValidatesEmail(request);

            Assert.IsTrue(_profileForgotPasswordResponseValidator.IsValid);
            Assert.IsTrue(_profileForgotPasswordResponseValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Forgot password validation Invalid return")]
        public void ValidatesEmailForgotPassword_ShouldInvalidReturn_Validation()
        {
            var request = ProfileMock.ForgotPasswordRequestInvalidMock();

            _profileForgotPasswordResponseValidator.ValidatesEmail(request);

            Assert.IsFalse(_profileForgotPasswordResponseValidator.IsValid);
            Assert.IsTrue(_profileForgotPasswordResponseValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Forgot password validation Invalid return empty")]
        public void ValidatesEmailForgotPassword_ShouldInvalidReturn_Empty()
        {
            var request = ProfileMock.ForgotPasswordRequestEmptyMock();

            _profileForgotPasswordResponseValidator.ValidatesEmail(request);

            Assert.IsFalse(_profileForgotPasswordResponseValidator.IsValid);
            Assert.IsTrue(_profileForgotPasswordResponseValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Forgot password validation Invalid return nullable")]
        public void ValidatesEmailForgotPassword_ShouldInvalidReturn_Nulable()
        {
            _profileForgotPasswordResponseValidator.ValidatesEmail(new ForgotPasswordRequest());

            Assert.IsFalse(_profileForgotPasswordResponseValidator.IsValid);
            Assert.IsTrue(_profileForgotPasswordResponseValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Forgot password validation Invalid return excpetion")]
        public void ValidatesEmailForgotPassword_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _profileForgotPasswordResponseValidator.ValidatesEmail(It.IsAny<ForgotPasswordRequest>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'request')");
        }

        #endregion

        #region Generate confirmation code

        [TestMethod("Generate confirmation code validation Correct return")]
        public void ValidatesEmailGenerateConfirmationCode_ShouldCorrectReturn_Validation()
        {
            var request = ProfileMock.GenerateConfirmationCodeRequestMock();

            _profileGenerateConfirmationCodeValidator.ValidatesEmail(request);

            Assert.IsTrue(_profileGenerateConfirmationCodeValidator.IsValid);
            Assert.IsTrue(_profileGenerateConfirmationCodeValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Generate confirmation code validation Invalid return")]
        public void ValidatesEmailGenerateConfirmationCode_ShouldInvalidReturn_Validation()
        {
            var request = ProfileMock.GenerateConfirmationCodeRequestInvalidMock();

            _profileGenerateConfirmationCodeValidator.ValidatesEmail(request);

            Assert.IsFalse(_profileGenerateConfirmationCodeValidator.IsValid);
            Assert.IsTrue(_profileGenerateConfirmationCodeValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Generate confirmation code validation Invalid return empty")]
        public void ValidatesEmailGenerateConfirmationCode_ShouldInvalidReturn_Empty()
        {
            var request = ProfileMock.GenerateConfirmationCodeRequestEmptyMock();

            _profileGenerateConfirmationCodeValidator.ValidatesEmail(request);

            Assert.IsFalse(_profileGenerateConfirmationCodeValidator.IsValid);
            Assert.IsTrue(_profileGenerateConfirmationCodeValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Generate confirmation code validation Invalid return nullable")]
        public void ValidatesEmailGenerateConfirmationCode_ShouldInvalidReturn_Nulable()
        {
            _profileGenerateConfirmationCodeValidator.ValidatesEmail(new GenerateConfirmationCodeRequest());

            Assert.IsFalse(_profileGenerateConfirmationCodeValidator.IsValid);
            Assert.IsTrue(_profileGenerateConfirmationCodeValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Generate confirmation code validation Invalid return excpetion")]
        public void ValidatesEmailGenerateConfirmationCode_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _profileGenerateConfirmationCodeValidator.ValidatesEmail(It.IsAny<GenerateConfirmationCodeRequest>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'request')");
        }

        #endregion

        #region Reset password

        [TestMethod("Reset password validation Correct return")]
        public void ValidatesResetPassword_ShouldCorrectReturn_Validation()
        {
            var request = ProfileMock.ResetPasswordRequestMock();

            _profileResetPasswordValidator.ValidatesResetPassword(request);

            Assert.IsTrue(_profileResetPasswordValidator.IsValid);
            Assert.IsTrue(_profileResetPasswordValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Reset password validation Invalid return")]
        public void ValidatesResetPassword_ShouldInvalidReturn_Validation()
        {
            var request = ProfileMock.ResetPasswordRequestInvalidMock();

            _profileResetPasswordValidator.ValidatesResetPassword(request);

            Assert.IsFalse(_profileResetPasswordValidator.IsValid);
            Assert.IsTrue(_profileResetPasswordValidator.Errors.Count.Equals(3));
        }

        [TestMethod("Reset password validation Invalid return empty")]
        public void ValidatesResetPassword_ShouldInvalidReturn_Empty()
        {
            var request = ProfileMock.ResetPasswordRequestEmptyMock();

            _profileResetPasswordValidator.ValidatesResetPassword(request);

            Assert.IsFalse(_profileResetPasswordValidator.IsValid);
            Assert.IsTrue(_profileResetPasswordValidator.Errors.Count.Equals(3));
        }

        [TestMethod("Reset password validation Invalid return nullable")]
        public void ValidatesResetPassword_ShouldInvalidReturn_Nulable()
        {
            _profileResetPasswordValidator.ValidatesResetPassword(new ResetPasswordRequest());

            Assert.IsFalse(_profileResetPasswordValidator.IsValid);
            Assert.IsTrue(_profileResetPasswordValidator.Errors.Count.Equals(3));
        }

        [TestMethod("Reset password validation Invalid return excpetion")]
        public void ValidatesResetPassword_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _profileResetPasswordValidator.ValidatesResetPassword(It.IsAny<ResetPasswordRequest>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'request')");
        }

        #endregion

        #region Validates email

        [TestMethod("Validates email validation Correct return")]
        public void ValidatesEmail_ShouldCorrectReturn_Validation()
        {
            var email = "teste@teste.com.zz";

            _profileResetPasswordValidator.ValidatesEmail(email);

            Assert.IsTrue(_profileResetPasswordValidator.IsValid);
            Assert.IsTrue(_profileResetPasswordValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Validates email validation Invalid return")]
        public void ValidatesEmail_ShouldInvalidReturn_Validation()
        {
            var email = "testetestecomzz";

            _profileResetPasswordValidator.ValidatesEmail(email);

            Assert.IsFalse(_profileResetPasswordValidator.IsValid);
            Assert.IsTrue(_profileResetPasswordValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Validates email validation Invalid return empty")]
        public void ValidatesEmail_ShouldInvalidReturn_Empty()
        {
            var email = string.Empty;

            _profileResetPasswordValidator.ValidatesEmail(email);

            Assert.IsFalse(_profileResetPasswordValidator.IsValid);
            Assert.IsTrue(_profileResetPasswordValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Validates email validation Invalid return nullable")]
        public void ValidatesEmail_ShouldInvalidReturn_Nulable()
        {
            _profileResetPasswordValidator.ValidatesEmail(It.IsAny<string>());

            Assert.IsFalse(_profileResetPasswordValidator.IsValid);
            Assert.IsTrue(_profileResetPasswordValidator.Errors.Count.Equals(1));
        }

        #endregion

        #region Email confirmation code expiration time

        [TestMethod("Email confirmation code expiration time validation Correct return")]
        public void ValidatesEmailConfirmationCodeExpirationTime_ShouldCorrectReturn_Validation()
        {
            var date = DateTime.Now;

            _profileActivationResponseValidator.ValidatesEmailConfirmationCodeExpirationTime(date);

            Assert.IsTrue(_profileActivationResponseValidator.IsValid);
            Assert.IsTrue(_profileActivationResponseValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Email confirmation code expiration time validation Invalid return")]
        public void ValidatesEmailConfirmationCodeExpirationTime_ShouldInvalidReturn_Validation()
        {
            var date = DateTime.Now.AddMinutes(Constant.CONFIRMATION_CODE_EXPIRATION_TIME_EMAIL).AddSeconds(-1);

            _profileActivationResponseValidator.ValidatesEmailConfirmationCodeExpirationTime(date);

            Assert.IsFalse(_profileActivationResponseValidator.IsValid);
            Assert.IsTrue(_profileActivationResponseValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Email confirmation code expiration time validation Invalid return nullable")]
        public void ValidatesEmailConfirmationCodeExpirationTime_ShouldInvalidReturn_Nulable()
        {
            _profileActivationResponseValidator.ValidatesEmailConfirmationCodeExpirationTime(It.IsAny<DateTime>());

            Assert.IsFalse(_profileActivationResponseValidator.IsValid);
            Assert.IsTrue(_profileActivationResponseValidator.Errors.Count.Equals(1));
        }

        #endregion

        #region Password confirmation code expiration time

        [TestMethod("Password confirmation code expiration time validation Correct return")]
        public void ValidatesPasswordConfirmationCodeExpirationTime_ShouldCorrectReturn_Validation()
        {
            var date = DateTime.Now;

            _profileActivationResponseValidator.ValidatesPasswordConfirmationCodeExpirationTime(date);

            Assert.IsTrue(_profileActivationResponseValidator.IsValid);
            Assert.IsTrue(_profileActivationResponseValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Password confirmation code expiration time validation Invalid return")]
        public void ValidatesPasswordConfirmationCodeExpirationTime_ShouldInvalidReturn_Validation()
        {
            var date = DateTime.Now.AddMinutes(Constant.CONFIRMATION_CODE_EXPIRATION_TIME_PASSWORD).AddSeconds(-1);

            _profileActivationResponseValidator.ValidatesPasswordConfirmationCodeExpirationTime(date);

            Assert.IsFalse(_profileActivationResponseValidator.IsValid);
            Assert.IsTrue(_profileActivationResponseValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Password confirmation code expiration time validation Invalid return nullable")]
        public void ValidatesPasswordConfirmationCodeExpirationTime_ShouldInvalidReturn_Nulable()
        {
            _profileActivationResponseValidator.ValidatesPasswordConfirmationCodeExpirationTime(It.IsAny<DateTime>());

            Assert.IsFalse(_profileActivationResponseValidator.IsValid);
            Assert.IsTrue(_profileActivationResponseValidator.Errors.Count.Equals(1));
        }

        #endregion
    }
}