using JaVisitei.Brasil.Business.ViewModels.Response.Profile;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.Service.Services;
using JaVisitei.Brasil.Business.Service.Test.Mocks;
using JaVisitei.Brasil.Data.Entities;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Moq;
using System;
using JaVisitei.Brasil.Helper.Others;

namespace JaVisitei.Brasil.Business.Service.Test.Services
{
    [TestClass]
    public class ProfileServiceTest
    {
        private ProfileService _profileService;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<IUserManagerService> _mockUserManagerService;
        private readonly Mock<IUserService> _mockUserService;

        public ProfileServiceTest()
        {
            _mockEmailService = new Mock<IEmailService>();
            _mockUserManagerService = new Mock<IUserManagerService>();
            _mockUserService = new Mock<IUserService>();

            Environment.SetEnvironmentVariable("JWT_EXPIDED_MINUTE", "2");
            Environment.SetEnvironmentVariable("JWT_AUDIENCE", "audience");
            Environment.SetEnvironmentVariable("JWT_ISSUER", "issuer");
            Environment.SetEnvironmentVariable("JWT_KEY", "teste@teste.com.br");
            Environment.SetEnvironmentVariable("JWT_SUBJECT", "subject");
        }

        #region Login

        [TestMethod("Login Correct return")]
        public async Task LoginAsync_ShouldCorrectReturn_Login()
        {
            var loginRequest = ProfileMock.LoginRequestMock();
            var loginValidation = ProfileMock.ProfileLoginResponseMock();
            var userResponse = UserMock.UserContributorMock();

            _profileService = new ProfileService(_mockUserService.Object, 
                loginValidation, null, null, null, null, null, null);

            _ = _mockUserService
                .Setup(x => x.LoginAsync<User>(loginRequest.Email, Encrypt.Sha256encrypt(loginRequest.Password)))
                .ReturnsAsync(userResponse);

            var result = await _profileService.LoginAsync(loginRequest);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(string.IsNullOrEmpty(result.Data.Id));
            Assert.IsFalse(string.IsNullOrEmpty(result.Data.Token));
            Assert.IsNotNull(result.Data.Expiration);
            Assert.AreEqual(loginValidation.Data, result.Data);
        }

        [TestMethod("Login Invalid validation")]
        public async Task LoginAsync_ShouldInvalidReturn_Validation()
        {
            var loginRequest = ProfileMock.LoginRequestInvalidMock();
            var loginValidation = ProfileMock.ProfileValidatorErrorMock<LoginResponse>();
            _profileService = new ProfileService(null, loginValidation, null, null, null, null, null, null);

            var result = await _profileService.LoginAsync(loginRequest);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
            Assert.AreEqual(loginValidation, result);
        }

        [TestMethod("Login Invalid return user")]
        public async Task LoginAsync_ShouldInvalidReturn_InvalidUser()
        {
            var loginRequest = ProfileMock.LoginRequestMock();
            var loginValidation = ProfileMock.ProfileValidatorErrorsMock<LoginResponse>();
            _profileService = new ProfileService(_mockUserService.Object,
                loginValidation, null, null, null, null, null, null);

            _ = _mockUserService
                .Setup(x => x.LoginAsync<User>(loginRequest.Email, loginRequest.Password))
                .ReturnsAsync((User)null);

            var result = await _profileService.LoginAsync(loginRequest);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
            Assert.AreEqual(loginValidation, result);
        }

        [TestMethod("Login Invalid return password")]
        public async Task LoginAsync_ShouldInvalidReturn_InvalidPassword()
        {
            var loginRequest = ProfileMock.LoginRequestMock();
            var loginValidation = ProfileMock.ProfileValidatorErrorsMock<LoginResponse>();
            var userResponse = UserMock.UserEmptyPasswordMock();
            _profileService = new ProfileService(_mockUserService.Object,
                loginValidation, null, null, null, null, null, null);

            _ = _mockUserService
                .Setup(x => x.LoginAsync<User>(loginRequest.Email, loginRequest.Password))
                .ReturnsAsync(userResponse);

            var result = await _profileService.LoginAsync(loginRequest);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
            Assert.AreEqual(loginValidation, result);
        }

        [TestMethod("Login Inactive user")]
        public async Task LoginAsync_ShouldInvalidReturn_InactiveUser()
        {
            var loginRequest = ProfileMock.LoginRequestMock();
            var loginValidation = ProfileMock.ProfileValidatorErrorsMock<LoginResponse>();
            var userResponse = UserMock.UserInactiveUserMock();
            _profileService = new ProfileService(_mockUserService.Object,
                loginValidation, null, null, null, null, null, null);

            _ = _mockUserService
                .Setup(x => x.LoginAsync<User>(loginRequest.Email, loginRequest.Password))
                .ReturnsAsync(userResponse);

            var result = await _profileService.LoginAsync(loginRequest);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
            Assert.AreEqual(loginValidation, result);
        }

        [TestMethod("Login Return user exception")]
        public async Task LoginAsync_ShouldProbrem_EmailException()
        {
            var loginRequest = ProfileMock.LoginRequestMock();
            var message = "Exception test";
            _profileService = new ProfileService(_mockUserService.Object,
                new ProfileValidator<LoginResponse>(), null, null, null, null, null, null);

            _ = _mockUserService
                .Setup(x => x.LoginAsync<User>(loginRequest.Email, Encrypt.Sha256encrypt(loginRequest.Password)))
                .Throws(new Exception(message));

            try
            {
                var result = await _profileService.LoginAsync(loginRequest);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Exception test");
            }
        }

        #endregion

        #region Active account 

        [TestMethod("Active account Correct return")]
        public async Task ActiveAccountAsync_ShouldCorretReturn_Activated()
        {
            var request = ProfileMock.ActiveAccountRequestMock();
            var responseUserManager = UserManagerMock.ReturnUserManagerMock();
            var responseUser = UserMock.UserContributorMock();
            var profileValidation = ProfileMock.ProfileActivationResponseMock();
            _profileService = new ProfileService(_mockUserService.Object,
                null, profileValidation, null, null, null, _mockUserManagerService.Object, null);

            _ = _mockUserManagerService
                .Setup(x => x.GetByManagerCodeAsync(request.ActivationCode))
                .ReturnsAsync(responseUserManager);

            _ = _mockUserManagerService
                .Setup(x => x.ConfirmedChangeAsync(responseUserManager))
                .ReturnsAsync(true);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Id.Equals(responseUserManager.UserId), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserService
                .Setup(x => x.UpdateAsync(responseUser))
                .ReturnsAsync(true);

            var result = await _profileService.ActiveAccountAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(profileValidation.Data.Actived, result.Data.Actived);
            Assert.AreEqual(profileValidation.Data.UserEmail, result.Data.UserEmail);
        }

        [TestMethod("Active account Invalid return validation")]
        public async Task ActiveAccountAsync_ShouldInvalidReturn_Validation()
        {
            var requestInvalid = ProfileMock.ActiveAccountRequestInvalidMock();
            var profileValidationInvalid = ProfileMock.ProfileValidatorErrorMock<ActivationResponse>();
            _profileService = new ProfileService(null, null, profileValidationInvalid, null, null, null, null, null);

            var result = await _profileService.ActiveAccountAsync(requestInvalid);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Active account Invalid return user manager nullable")]
        public async Task ActiveAccountAsync_ShouldInvalidReturn_UserManagerNullable()
        {
            var request = ProfileMock.ActiveAccountRequestMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<ActivationResponse>();
            _profileService = new ProfileService(null, null, profileValidation, null, null, null, _mockUserManagerService.Object, null);

            _ = _mockUserManagerService
                .Setup(x => x.GetByManagerCodeAsync(request.ActivationCode))
                .ReturnsAsync((UserManager)null);

            var result = await _profileService.ActiveAccountAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Active account Invalid return expiration date")]
        public async Task ActiveAccountAsync_ShouldInvalidReturn_ExpirationDate()
        {
            var request = ProfileMock.ActiveAccountRequestMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<ActivationResponse>();
            var responseUserManager = UserManagerMock.ReturnUserManagerExpirationDateMock();
            _profileService = new ProfileService(null, null, profileValidation, null, null, null, _mockUserManagerService.Object, null);

            _ = _mockUserManagerService
                .Setup(x => x.GetByManagerCodeAsync(request.ActivationCode))
                .ReturnsAsync(responseUserManager);

            var result = await _profileService.ActiveAccountAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Active account Invalid confirmation change")]
        public async Task ActiveAccountAsync_ShouldInvalidReturn_ConfirmationChange()
        {
            var request = ProfileMock.ActiveAccountRequestMock();
            var responseUserManager = UserManagerMock.ReturnUserManagerMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<ActivationResponse>();
            _profileService = new ProfileService(null, null, profileValidation, null, null, null, _mockUserManagerService.Object, null);

            _ = _mockUserManagerService
                .Setup(x => x.GetByManagerCodeAsync(request.ActivationCode))
                .ReturnsAsync(responseUserManager);

            _ = _mockUserManagerService
                .Setup(x => x.ConfirmedChangeAsync(It.IsAny<UserManager>()))
                .ReturnsAsync(false);

            var result = await _profileService.ActiveAccountAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Active account Invalid return user nullable")]
        public async Task ActiveAccountAsync_ShouldCorretReturn_UserNullable()
        {
            var request = ProfileMock.ActiveAccountRequestMock();
            var responseUserManager = UserManagerMock.ReturnUserManagerMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<ActivationResponse>();
            _profileService = new ProfileService(_mockUserService.Object,
                null, profileValidation, null, null, null, _mockUserManagerService.Object, null);

            _ = _mockUserManagerService
                .Setup(x => x.GetByManagerCodeAsync(request.ActivationCode))
                .ReturnsAsync(responseUserManager);

            _ = _mockUserManagerService
                .Setup(x => x.ConfirmedChangeAsync(responseUserManager))
                .ReturnsAsync(true);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Id.Equals(responseUserManager.UserId), null))
                .ReturnsAsync((User)null);

            var result = await _profileService.ActiveAccountAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Active account Invalid user update")]
        public async Task ActiveAccountAsync_ShouldCorretReturn_UserUpdate()
        {
            var request = ProfileMock.ActiveAccountRequestMock();
            var responseUserManager = UserManagerMock.ReturnUserManagerMock();
            var responseUser = UserMock.UserContributorMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<ActivationResponse>();
            _profileService = new ProfileService(_mockUserService.Object,
                null, profileValidation, null, null, null, _mockUserManagerService.Object, null);

            _ = _mockUserManagerService
                .Setup(x => x.GetByManagerCodeAsync(request.ActivationCode))
                .ReturnsAsync(responseUserManager);

            _ = _mockUserManagerService
                .Setup(x => x.ConfirmedChangeAsync(responseUserManager))
                .ReturnsAsync(true);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Id.Equals(responseUserManager.UserId), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserService
                .Setup(x => x.UpdateAsync(responseUser))
                .ReturnsAsync(false);

            var result = await _profileService.ActiveAccountAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Active account Return exception")]
        public async Task ActiveAccountAsync_ShouldProbrem_Exception()
        {
            var request = ProfileMock.ActiveAccountRequestMock();
            var message = "Exception test";
            _profileService = new ProfileService(null, null, new ProfileValidator<ActivationResponse>(), 
                null, null, null, _mockUserManagerService.Object, null);

            _ = _mockUserManagerService
                .Setup(x => x.GetByManagerCodeAsync(request.ActivationCode))
                .Throws(new Exception(message));

            try
            {
                var result = await _profileService.ActiveAccountAsync(request);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Exception test");
            }
        }

        #endregion

        #region Generate confirmation code 

        [TestMethod("Generate confirmation code Correct return")]
        public async Task GenerateConfirmationCodeAsync_ShouldCorretReturn_CodeGenerate()
        {
            var request = ProfileMock.GenerateConfirmationCodeRequestMock();
            var profileValidation = ProfileMock.ProfileGenerateConfirmationCodeResponseMock();
            var responseUser = UserMock.UserInactiveUserMock();
            var responseUserManager = UserManagerMock.ReturnUserManagerMock();
            var emailValidation = EmailMock.ReturnEmailUserManagerResponseMock();
            responseUserManager.User = responseUser;
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, null, null, profileValidation, _mockUserManagerService.Object, _mockEmailService.Object);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserManagerService
                .Setup(x => x.CreateEmailConfirmationAsync(responseUser.Id))
                .ReturnsAsync(responseUserManager);

            _ = _mockEmailService
                .Setup(x => x.SendEmailUserManagerAsync(responseUserManager))
                .ReturnsAsync(emailValidation);

            var result = await _profileService.GenerateConfirmationCodeAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(request.Email, result.Data.UserEmail);
            Assert.AreEqual(responseUser.Email, result.Data.UserEmail);
            Assert.IsTrue(result.Data.Generated);
        }

        [TestMethod("Generate confirmation code Return invalid send")]
        public async Task GenerateConfirmationCodeAsync_ShouldInvalidReturn_InvalidSend()
        {
            var request = ProfileMock.GenerateConfirmationCodeRequestMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<GenerateConfirmationCodeResponse>();
            var responseUser = UserMock.UserInactiveUserMock();
            var responseUserManager = UserManagerMock.ReturnUserManagerMock();
            var emailValidationInvalid = EmailMock.ReturnEmailUserManagerInvalidMock();
            responseUserManager.User = responseUser;
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, null, null, profileValidation, _mockUserManagerService.Object, _mockEmailService.Object);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserManagerService
                .Setup(x => x.CreateEmailConfirmationAsync(responseUser.Id))
                .ReturnsAsync(responseUserManager);

            _ = _mockEmailService
                .Setup(x => x.SendEmailUserManagerAsync(responseUserManager))
                .ReturnsAsync(emailValidationInvalid);

            var result = await _profileService.GenerateConfirmationCodeAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Generate confirmation code Return nullable user manager")]
        public async Task GenerateConfirmationCodeAsync_ShouldInvalidReturn_UserManagerNullable()
        {
            var request = ProfileMock.GenerateConfirmationCodeRequestMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<GenerateConfirmationCodeResponse>();
            var responseUser = UserMock.UserInactiveUserMock();
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, null, null, profileValidation, _mockUserManagerService.Object, null);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserManagerService
                .Setup(x => x.CreateEmailConfirmationAsync(responseUser.Id))
                .ReturnsAsync((UserManager)null);

            var result = await _profileService.GenerateConfirmationCodeAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Generate confirmation code Return invalid user actived")]
        public async Task GenerateConfirmationCodeAsync_ShouldInvalidReturn_UserActived()
        {
            var request = ProfileMock.GenerateConfirmationCodeRequestMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<GenerateConfirmationCodeResponse>();
            var responseUser = UserMock.UserContributorMock();
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, null, null, profileValidation, null, null);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .ReturnsAsync(responseUser);

            var result = await _profileService.GenerateConfirmationCodeAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
        }

        [TestMethod("Generate confirmation code Return nullable user")]
        public async Task GenerateConfirmationCodeAsync_ShouldInvalidReturn_UserNullable()
        {
            var request = ProfileMock.GenerateConfirmationCodeRequestMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<GenerateConfirmationCodeResponse>();
            var responseUser = UserMock.UserInactiveUserMock();
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, null, null, profileValidation, null, null);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .ReturnsAsync((User)null);

            var result = await _profileService.GenerateConfirmationCodeAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Generate confirmation code return validation")]
        public async Task GenerateConfirmationCodeAsync_ShouldInvalidReturn_Validation()
        {
            var requestInvalid = ProfileMock.GenerateConfirmationCodeRequestInvalidMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorMock<GenerateConfirmationCodeResponse>();
            _profileService = new ProfileService(null, null, null, null, null, profileValidation, null, null);

            var result = await _profileService.GenerateConfirmationCodeAsync(requestInvalid);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Generate confirmation code Return exception")]
        public async Task GenerateConfirmationCodeAsync_ShouldProbrem_Exception()
        {
            var request = ProfileMock.GenerateConfirmationCodeRequestMock();
            var message = "Exception test";
            _profileService = new ProfileService(_mockUserService.Object, 
                null, null, null, null, new ProfileValidator<GenerateConfirmationCodeResponse>(), null, null);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .Throws(new Exception(message));

            try
            {
                var result = await _profileService.GenerateConfirmationCodeAsync(request);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Exception test");
            }
        }

        #endregion

        #region Forgot password

        [TestMethod("Forgot password code Correct return")]
        public async Task ForgotPasswordAsync_ShouldCorretReturn_CodeGenerate()
        {
            var request = ProfileMock.ForgotPasswordRequestMock();
            var profileValidation = ProfileMock.ProfileForgotPasswordResponseMock();
            var responseUser = UserMock.UserContributorMock();
            var responseUserManager = UserManagerMock.ReturnUserManagerMock();
            var emailValidation = EmailMock.ReturnEmailUserManagerResponseMock();
            responseUserManager.User = responseUser;
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, profileValidation, null, null, _mockUserManagerService.Object, _mockEmailService.Object);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserManagerService
                .Setup(x => x.CreatePasswordResetAsync(responseUser.Id))
                .ReturnsAsync(responseUserManager);

            _ = _mockEmailService
                .Setup(x => x.SendEmailUserManagerAsync(responseUserManager))
                .ReturnsAsync(emailValidation);

            var result = await _profileService.ForgotPasswordAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(request.Email, result.Data.UserEmail);
            Assert.AreEqual(responseUser.Email, result.Data.UserEmail);
            Assert.IsTrue(result.Data.Requested);
        }

        [TestMethod("Forgot password Return invalid send")]
        public async Task ForgotPasswordAsync_ShouldInvalidReturn_InvalidSend()
        {
            var request = ProfileMock.ForgotPasswordRequestMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<ForgotPasswordResponse>();
            var responseUser = UserMock.UserContributorMock();
            var responseUserManager = UserManagerMock.ReturnUserManagerMock();
            var emailValidationInvalid = EmailMock.ReturnEmailUserManagerInvalidMock();
            responseUserManager.User = responseUser;
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, profileValidation, null, null, _mockUserManagerService.Object, _mockEmailService.Object);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserManagerService
                .Setup(x => x.CreateEmailConfirmationAsync(responseUser.Id))
                .ReturnsAsync(responseUserManager);

            _ = _mockEmailService
                .Setup(x => x.SendEmailUserManagerAsync(responseUserManager))
                .ReturnsAsync(emailValidationInvalid);

            var result = await _profileService.ForgotPasswordAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Forgot password Return nullable user manager")]
        public async Task ForgotPasswordAsync_ShouldInvalidReturn_UserManagerNullable()
        {
            var request = ProfileMock.ForgotPasswordRequestMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<ForgotPasswordResponse>();
            var responseUser = UserMock.UserContributorMock();
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, profileValidation, null, null, _mockUserManagerService.Object, null);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserManagerService
                .Setup(x => x.CreateEmailConfirmationAsync(responseUser.Id))
                .ReturnsAsync((UserManager)null);

            var result = await _profileService.ForgotPasswordAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Forgot password Return nullable user")]
        public async Task ForgotPasswordAsync_ShouldInvalidReturn_UserNullable()
        {
            var request = ProfileMock.ForgotPasswordRequestMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<ForgotPasswordResponse>();
            var responseUser = UserMock.UserContributorMock();
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, profileValidation, null, null, null, null);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .ReturnsAsync((User)null);

            var result = await _profileService.ForgotPasswordAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Forgot password return validation")]
        public async Task ForgotPasswordAsync_ShouldInvalidReturn_Validation()
        {
            var requestInvalid = ProfileMock.ForgotPasswordRequestInvalidMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorMock<ForgotPasswordResponse>();
            _profileService = new ProfileService(null, null, null, profileValidation, null, null, null, null);

            var result = await _profileService.ForgotPasswordAsync(requestInvalid);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Forgot password Return exception")]
        public async Task ForgotPasswordAsync_ShouldProbrem_Exception()
        {
            var request = ProfileMock.ForgotPasswordRequestMock();
            var message = "Exception test";
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, new ProfileValidator<ForgotPasswordResponse>(), null, null, null, null);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .Throws(new Exception(message));

            try
            {
                var result = await _profileService.ForgotPasswordAsync(request);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Exception test");
            }
        }

        #endregion

        #region Reset password

        [TestMethod("Reset password Correct return")]
        public async Task ResetPasswordAsync_ShouldCorretReturn_CodeGenerate()
        {
            var request = ProfileMock.ResetPasswordRequestMock();
            var profileValidation = ProfileMock.ProfileResetPasswordResponseMock();
            var responseUser = UserMock.UserContributorMock();
            var responseUserManager = UserManagerMock.ReturnUserManagerNotConfirmedMock();
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, null, profileValidation, null, _mockUserManagerService.Object, null);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserManagerService
                .Setup(x => x.GetByManagerCodeAsync(request.ResetPasswordCode))
                .ReturnsAsync(responseUserManager);

            _ = _mockUserService
                .Setup(x => x.UpdateAsync(responseUser))
                .ReturnsAsync(true);

            _ = _mockUserManagerService
                .Setup(x => x.ConfirmedChangeAsync(responseUserManager))
                .ReturnsAsync(true);

            var result = await _profileService.ResetPasswordAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(request.Email, result.Data.UserEmail);
            Assert.AreEqual(responseUser.Email, result.Data.UserEmail);
            Assert.IsTrue(result.Data.Redefined);
        }

        [TestMethod("Reset password Invalid confirmed change")]
        public async Task ResetPasswordAsync_ShouldInvalidReturn_InvalidConfirmedChange()
        {
            var request = ProfileMock.ResetPasswordRequestMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<ResetPasswordResponse>();
            var responseUser = UserMock.UserContributorMock();
            var responseUserManager = UserManagerMock.ReturnUserManagerNotConfirmedMock();
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, null, profileValidation, null, _mockUserManagerService.Object, null);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserManagerService
                .Setup(x => x.GetByManagerCodeAsync(request.ResetPasswordCode))
                .ReturnsAsync(responseUserManager);

            _ = _mockUserService
                .Setup(x => x.UpdateAsync(responseUser))
                .ReturnsAsync(true);

            _ = _mockUserManagerService
                .Setup(x => x.ConfirmedChangeAsync(responseUserManager))
                .ReturnsAsync(false);

            var result = await _profileService.ResetPasswordAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Reset password Invalid user update")]
        public async Task ResetPasswordAsync_ShouldInvalidReturn_InvalidUserUpdate()
        {
            var request = ProfileMock.ResetPasswordRequestMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<ResetPasswordResponse>();
            var responseUser = UserMock.UserContributorMock();
            var responseUserManager = UserManagerMock.ReturnUserManagerNotConfirmedMock();
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, null, profileValidation, null, _mockUserManagerService.Object, null);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserManagerService
                .Setup(x => x.GetByManagerCodeAsync(request.ResetPasswordCode))
                .ReturnsAsync(responseUserManager);

            _ = _mockUserService
                .Setup(x => x.UpdateAsync(responseUser))
                .ReturnsAsync(false);

            var result = await _profileService.ResetPasswordAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Reset password Invalid user manager nullable")]
        public async Task ResetPasswordAsync_ShouldInvalidReturn_InvalidUserManagerNullable()
        {
            var request = ProfileMock.ResetPasswordRequestMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<ResetPasswordResponse>();
            var responseUser = UserMock.UserContributorMock();
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, null, profileValidation, null, _mockUserManagerService.Object, null);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserManagerService
                .Setup(x => x.GetByManagerCodeAsync(request.ResetPasswordCode))
                .ReturnsAsync((UserManager)null);

            var result = await _profileService.ResetPasswordAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }


        [TestMethod("Reset password Invalid user nullable")]
        public async Task ResetPasswordAsync_ShouldInvalidReturn_InvalidUserNullable()
        {
            var request = ProfileMock.ResetPasswordRequestMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorsMock<ResetPasswordResponse>();
            var responseUser = UserMock.UserContributorMock();
            _profileService = new ProfileService(_mockUserService.Object,
                null, null, null, profileValidation, null, null, null);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .ReturnsAsync((User)null);

            var result = await _profileService.ResetPasswordAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Reset password Return validation")]
        public async Task ResetPasswordAsync_ShouldInvalidReturn_Validation()
        {
            var requestInvalid = ProfileMock.ResetPasswordRequestInvalidMock();
            var profileValidation = ProfileMock.ProfileValidatorErrorMock<ResetPasswordResponse>();
            _profileService = new ProfileService(null, null, null, null, profileValidation, null, null, null);

            var result = await _profileService.ResetPasswordAsync(requestInvalid);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
        }

        [TestMethod("Reset password Return exception")]
        public async Task ResetPasswordAsync_ShouldProbrem_Exception()
        {
            var request = ProfileMock.ResetPasswordRequestMock();
            var message = "Exception test";
            _profileService = new ProfileService(_mockUserService.Object, 
                null, null, null, new ProfileValidator<ResetPasswordResponse>(), null, null, null);

            _ = _mockUserService
                .Setup(x => x.GetFirstOrDefaultAsync(m => m.Email.Equals(request.Email.ToLower()), null))
                .Throws(new Exception(message));

            try
            {
                var result = await _profileService.ResetPasswordAsync(request);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Exception test");
            }
        }

        #endregion
    }
}
