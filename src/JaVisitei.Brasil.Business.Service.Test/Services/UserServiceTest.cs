using JaVisitei.Brasil.Business.Service.Services;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JaVisitei.Brasil.Business.Service.Test.Mocks;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.ViewModels.Response.User;
using JaVisitei.Brasil.Data.Entities;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Service.Test.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IUserManagerService> _mockUserManagerService;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<IMapper> _mockMapper;

        public UserServiceTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserManagerService = new Mock<IUserManagerService>();
            _mockEmailService = new Mock<IEmailService>();
            _mockMapper = new Mock<IMapper>();

            _userService = new UserService(_mockUserRepository.Object,
                _mockUserManagerService.Object, 
                new UserValidator(),
                _mockEmailService.Object,
                _mockMapper.Object);
        }

        #region Insert user

        [TestMethod("Insert user Correct return")]
        public async Task InsertAsync_ShouldCorrectReturn_User()
        {
            var mapper = UserMock.CreateUserBasicFullRequestMock();

            _ = _mockMapper
                .Setup(x => x.Map<InsertFullUserRequest>(It.IsAny<InsertUserRequest>()))
                .Returns(mapper);

            var result = await _userService.InsertAsync(It.IsAny<InsertUserRequest>());

            Assert.IsNotNull(result);
            Assert.IsNull(result.Data);
        }

        #endregion

        #region Insert user full

        [TestMethod("Insert user Correct return")]
        public async Task InsertAsync_ShouldCorrectReturn_UserFull()
        {
            var request = UserMock.CreateUserBasicFullRequestMock();
            var mapperUser = UserMock.UserContributorMock();
            var mapperUserResponse = UserMock.UserInactiveContributorMock();
            var responseUser = UserMock.UserInactiveUserMock();
            var responseUserManager = UserManagerMock.ReturnUserManagerNotConfirmedMock();
            var emailValidation = EmailMock.ReturnEmailUserManagerResponseMock();
            var userRole = UserMock.UserRoleContributorMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Email.Equals(request.Email) || c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockMapper
                .Setup(x => x.Map<User>(request))
                .Returns(mapperUser);

            _ = _mockUserRepository
                .Setup(x => x.InsertAsync(mapperUser))
                .ReturnsAsync(true);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Email.Equals(mapperUser.Email), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserManagerService
                .Setup(x => x.CreateEmailConfirmationAsync(responseUser.Id))
                .ReturnsAsync(responseUserManager);

            _ = _mockEmailService
                .Setup(x => x.SendEmailUserManagerAsync(responseUser.Email, responseUserManager))
                .ReturnsAsync(emailValidation);

            _ = _mockMapper
                .Setup(x => x.Map<UserResponse>(responseUser))
                .Returns(mapperUserResponse);

            var result = await _userService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(mapperUserResponse, result.Data);
        }

        [TestMethod("Insert user full Invalid return data mapper user reponse")]
        public async Task InsertAsync_ShouldInvalidReturn_DataUserResponse()
        {
            var request = UserMock.CreateUserBasicFullRequestMock();
            var mapperUser = UserMock.UserContributorMock();
            var responseUser = UserMock.UserInactiveUserMock();
            var responseUserManager = UserManagerMock.ReturnUserManagerNotConfirmedMock();
            var emailValidation = EmailMock.ReturnEmailUserManagerResponseMock();
            var userRole = UserMock.UserRoleContributorMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Email.Equals(request.Email) || c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockMapper
                .Setup(x => x.Map<User>(request))
                .Returns(mapperUser);

            _ = _mockUserRepository
                .Setup(x => x.InsertAsync(mapperUser))
                .ReturnsAsync(true);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Email.Equals(mapperUser.Email), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserManagerService
                .Setup(x => x.CreateEmailConfirmationAsync(responseUser.Id))
                .ReturnsAsync(responseUserManager);

            _ = _mockEmailService
                .Setup(x => x.SendEmailUserManagerAsync(responseUser.Email, responseUserManager))
                .ReturnsAsync(emailValidation);

            _ = _mockMapper
                .Setup(x => x.Map<UserResponse>(responseUser))
                .Returns((UserResponse)null);

            var result = await _userService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
        }

        [TestMethod("Insert user full Invalid send email")]
        public async Task InsertAsync_ShouldInvalidReturn_SendEmailNullable()
        {
            var request = UserMock.CreateUserBasicFullRequestMock();
            var mapperUser = UserMock.UserContributorMock();
            var responseUser = UserMock.UserInactiveUserMock();
            var responseUserManager = UserManagerMock.ReturnUserManagerNotConfirmedMock();
            var emailValidationInvalid = EmailMock.ReturnEmailUserManagerInvalidMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Email.Equals(request.Email) || c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockMapper
                .Setup(x => x.Map<User>(request))
                .Returns(mapperUser);

            _ = _mockUserRepository
                .Setup(x => x.InsertAsync(mapperUser))
                .ReturnsAsync(true);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Email.Equals(mapperUser.Email), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserManagerService
                .Setup(x => x.CreateEmailConfirmationAsync(responseUser.Id))
                .ReturnsAsync(responseUserManager);

            _ = _mockEmailService
                .Setup(x => x.SendEmailUserManagerAsync(responseUser.Email, responseUserManager))
                .ReturnsAsync(emailValidationInvalid);

            var result = await _userService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert user full Invalid user manager created confirmation")]
        public async Task InsertAsync_ShouldInvalidReturn_UserManagerNullable()
        {
            var request = UserMock.CreateUserBasicFullRequestMock();
            var mapperUser = UserMock.UserContributorMock();
            var responseUser = UserMock.UserInactiveUserMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Email.Equals(request.Email) || c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockMapper
                .Setup(x => x.Map<User>(request))
                .Returns(mapperUser);

            _ = _mockUserRepository
                .Setup(x => x.InsertAsync(mapperUser))
                .ReturnsAsync(true);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Email.Equals(mapperUser.Email), null))
                .ReturnsAsync(responseUser);

            _ = _mockUserManagerService
                .Setup(x => x.CreateEmailConfirmationAsync(responseUser.Id))
                .ReturnsAsync((UserManager)null);

            var result = await _userService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert user full Invalid consult user by e-mail")]
        public async Task InsertAsync_ShouldInvalidReturn_ConsultUserNullable()
        {
            var request = UserMock.CreateUserBasicFullRequestMock();
            var mapperUser = UserMock.UserContributorMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Email.Equals(request.Email) || c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockMapper
                .Setup(x => x.Map<User>(request))
                .Returns(mapperUser);

            _ = _mockUserRepository
                .Setup(x => x.InsertAsync(mapperUser))
                .ReturnsAsync(true);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Email.Equals(mapperUser.Email), null))
                .ReturnsAsync((User)null);

            var result = await _userService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert user full Invalid insert user")]
        public async Task InsertAsync_ShouldInvalidReturn_InsertUserFalse()
        {
            var request = UserMock.CreateUserBasicFullRequestMock();
            var mapperUser = UserMock.UserContributorMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Email.Equals(request.Email) || c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockMapper
                .Setup(x => x.Map<User>(request))
                .Returns(mapperUser);

            _ = _mockUserRepository
                .Setup(x => x.InsertAsync(mapperUser))
                .ReturnsAsync(false);

            var result = await _userService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert user full Invalid mapper user")]
        public async Task InsertAsync_ShouldInvalidReturn_MapperUser()
        {
            var request = UserMock.CreateUserBasicFullRequestMock();
            var mapperUser = UserMock.UserContributorMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Email.Equals(request.Email) || c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockMapper
                .Setup(x => x.Map<User>(request))
                .Returns((User)null);

            var result = await _userService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert user full Invalid User exists")]
        public async Task InsertAsync_ShouldInvalidReturn_UserExists()
        {
            var request = UserMock.CreateUserBasicFullRequestMock();
            
            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Email.Equals(request.Email) || c.Username.Equals(request.Username)))
                .ReturnsAsync(true);

            var result = await _userService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert user full Invalid validation")]
        public async Task InsertAsync_ShouldInvalidReturn_Validation()
        {
            var request = UserMock.CreateUserInvalidRequestMock();
            var userValidationInvalid = UserMock.UserValidatorErrorMock();
            var userService = new UserService(null, null, userValidationInvalid, null, null);

            var result = await userService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Insert user full Return exception")]
        public async Task InsertAsync_ShouldProbrem_Exception()
        {
            var request = UserMock.CreateUserBasicFullRequestMock();
            var message = "Exception test";

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Email.Equals(request.Email) || c.Username.Equals(request.Username)))
                .Throws(new Exception(message));

            var result = await _userService.InsertAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors[0].Contains(message));
        }

        #endregion

        #region Update user

        [TestMethod("Update user Correct return")]
        public async Task UpdateAsync_ShouldCorrectReturn_User()
        {
            var mapper = UserMock.UpdateFullUserRequestMock();

            _ = _mockMapper
                .Setup(x => x.Map<UpdateFullUserRequest>(It.IsAny<UpdateUserRequest>()))
                .Returns(mapper);

            var result = await _userService.UpdateAsync(It.IsAny<UpdateUserRequest>());

            Assert.IsNotNull(result);
            Assert.IsNull(result.Data);
        }

        #endregion

        #region Update user full

        [TestMethod("Update user full Correct return")]
        public async Task UpdateAsync_ShouldCorrectReturn_UserFull()
        {
            var request = UserMock.UpdateFullUserRequestMock();
            var userResponse = UserMock.UserContributorMock();
            var mapperUser = UserMock.UserContributorModifiedMock();
            var user = UserMock.UserContributorModifiedMock();
            var mapperUserResponse = UserMock.UserActivedContributorMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Email.Equals(request.Email)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(c => c.GetFirstOrDefaultAsync(x => x.Id.Equals(request.Id), null))
                .ReturnsAsync(userResponse);

            _ = _mockUserRepository
                .Setup(x => x.LoginAsync(userResponse.Email, userResponse.Password))
                .ReturnsAsync(userResponse);

            _ = _mockMapper
                .Setup(x => x.Map<User>(request))
                .Returns(mapperUser);

            _ = _mockUserRepository
                .Setup(x => x.UpdateAsync(mapperUser))
                .ReturnsAsync(true);

            _ = _mockUserRepository
                .Setup(x => x.GetByIdAsync(mapperUser.Id))
                .ReturnsAsync(user);

            _ = _mockMapper
                .Setup(x => x.Map<UserResponse>(user))
                .Returns(mapperUserResponse);

            var result = await _userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(mapperUserResponse, result.Data);
        }

        [TestMethod("Update user full Correct return without password")]
        public async Task UpdateAsync_ShouldCorrectReturn_UserFullWithoutPassord()
        {
            var request = UserMock.UpdateFullUserPasswordNullRequestMock();
            var userResponse = UserMock.UserEmptyPasswordMock();
            var mapperUser = UserMock.UserContributorModifiedMock();
            var user = UserMock.UserContributorModifiedMock();
            var mapperUserResponse = UserMock.UserActivedContributorMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Email.Equals(request.Email)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Id.Equals(request.Id), null))
                .ReturnsAsync(userResponse);

            _ = _mockMapper
                .Setup(x => x.Map<User>(request))
                .Returns(mapperUser);

            _ = _mockUserRepository
                .Setup(x => x.UpdateAsync(mapperUser))
                .ReturnsAsync(true);

            _ = _mockUserRepository
                .Setup(x => x.GetByIdAsync(mapperUser.Id))
                .ReturnsAsync(user);

            _ = _mockMapper
                .Setup(x => x.Map<UserResponse>(user))
                .Returns(mapperUserResponse);

            var result = await _userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(mapperUserResponse, result.Data);
        }

        [TestMethod("Update user full Invalid return mapper user")]
        public async Task UpdateAsync_ShouldInvalidReturn_MapperUserFull()
        {
            var request = UserMock.UpdateFullUserPasswordNullRequestMock();
            var userResponse = UserMock.UserEmptyPasswordMock();
            var mapperUser = UserMock.UserContributorModifiedMock();
            var user = UserMock.UserContributorModifiedMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Email.Equals(request.Email)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Id.Equals(request.Id), null))
                .ReturnsAsync(userResponse);

            _ = _mockMapper
                .Setup(x => x.Map<User>(request))
                .Returns(mapperUser);

            _ = _mockUserRepository
                .Setup(x => x.UpdateAsync(mapperUser))
                .ReturnsAsync(true);

            _ = _mockUserRepository
                .Setup(x => x.GetByIdAsync(mapperUser.Id))
                .ReturnsAsync(user);

            _ = _mockMapper
                .Setup(x => x.Map<UserResponse>(user))
                .Returns((UserResponse)null);

            var result = await _userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
        }

        [TestMethod("Update user full Invalid return user nullable")]
        public async Task UpdateAsync_ShouldInvalidReturn_UserNullable()
        {
            var request = UserMock.UpdateFullUserPasswordNullRequestMock();
            var userResponse = UserMock.UserEmptyPasswordMock();
            var mapperUser = UserMock.UserContributorModifiedMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Email.Equals(request.Email)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Equals(request.Id), null))
                .ReturnsAsync(userResponse);

            _ = _mockMapper
                .Setup(x => x.Map<User>(request))
                .Returns(mapperUser);

            _ = _mockUserRepository
                .Setup(x => x.UpdateAsync(mapperUser))
                .ReturnsAsync(true);

            _ = _mockUserRepository
                .Setup(x => x.GetByIdAsync(mapperUser.Id))
                .ReturnsAsync((User)null);

            var result = await _userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Update user full Invalid return user update false")]
        public async Task UpdateAsync_ShouldInvalidReturn_UserUpdateFalse()
        {
            var request = UserMock.UpdateFullUserRequestMock();
            var userResponse = UserMock.UserContributorMock();
            var mapperUser = UserMock.UserContributorModifiedMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Email.Equals(request.Email)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Equals(request.Id), null))
                .ReturnsAsync(userResponse);

            _ = _mockUserRepository
                .Setup(x => x.LoginAsync(userResponse.Email, It.IsAny<string>()))
                .ReturnsAsync(userResponse);

            _ = _mockMapper
                .Setup(x => x.Map<User>(request))
                .Returns(mapperUser);

            _ = _mockUserRepository
                .Setup(x => x.UpdateAsync(mapperUser))
                .ReturnsAsync(false);

            var result = await _userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Update user full Invalid return user update false")]
        public async Task UpdateAsync_ShouldInvalidReturn_UserMapperErroPassword()
        {
            var request = UserMock.UpdateFullUserRequestMock();
            var userResponse = UserMock.UserContributorMock();
            var mapperUser = UserMock.UserContributorModifiedMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Email.Equals(request.Email)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Equals(request.Id), null))
                .ReturnsAsync(userResponse);

            _ = _mockUserRepository
                .Setup(x => x.LoginAsync(userResponse.Email, It.IsAny<string>()))
                .ReturnsAsync(userResponse);

            _ = _mockMapper
                .Setup(x => x.Map<User>(request))
                .Returns((User)null);

            var result = await _userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Update user full Invalid return user update without password")]
        public async Task UpdateAsync_ShouldInvalidReturn_UserUpdateWithoutPassword()
        {
            var request = UserMock.UpdateFullUserPasswordNullRequestMock();
            var userResponse = UserMock.UserEmptyPasswordMock();
            var mapperUser = UserMock.UserContributorModifiedMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Email.Equals(request.Email)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Equals(request.Id), null))
                .ReturnsAsync(userResponse);

            _ = _mockUserRepository
                .Setup(x => x.LoginAsync(userResponse.Email, It.IsAny<string>()))
                .ReturnsAsync(userResponse);

            _ = _mockMapper
                .Setup(x => x.Map<User>(request))
                .Returns(mapperUser);

            var result = await _userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Update user full Invalid return login is null")]
        public async Task UpdateAsync_ShouldInvalidReturn_LoginIsNull()
        {
            var request = UserMock.UpdateFullUserRequestMock();
            var userResponse = UserMock.UserContributorMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Email.Equals(request.Email)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Equals(request.Id), null))
                .ReturnsAsync(userResponse);

            _ = _mockUserRepository
                .Setup(x => x.LoginAsync(userResponse.Email, It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var result = await _userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Update user full Invalid old password")]
        public async Task UpdateAsync_ShouldInvalidReturn_InvalidOldPassword()
        {
            var request = UserMock.UpdateFullUserRequestMock();
            var userResponse = UserMock.UserBasicMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Email.Equals(request.Email)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Equals(request.Id), null))
                .ReturnsAsync(userResponse);

            var result = await _userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Update user full Invalid password nullable")]
        public async Task UpdateAsync_ShouldInvalidReturn_InvalidPasswordNullable()
        {
            var request = UserMock.UpdateFullUserRequestMock();
            var userResponse = UserMock.UserEmptyPasswordMock();

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Email.Equals(request.Email)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Equals(request.Id), null))
                .ReturnsAsync(userResponse);

            var result = await _userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Update user full Invalid return user by id")]
        public async Task UpdateAsync_ShouldInvalidReturn_GetByIdNullable()
        {
            var request = UserMock.UpdateFullUserPasswordNullRequestMock();
            
            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Email.Equals(request.Email)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.GetFirstOrDefaultAsync(c => c.Equals(request.Id), null))
                .ReturnsAsync((User)null);

            var result = await _userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Update user full Invalid return user by e-mail exists")]
        public async Task UpdateAsync_ShouldInvalidReturn_UserEmailExists()
        {
            var request = UserMock.UpdateFullUserPasswordNullRequestMock();
            
            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Username.Equals(request.Username)))
                .ReturnsAsync(false);

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Email.Equals(request.Email)))
                .ReturnsAsync(true);

            var result = await _userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Update user full Invalid return user by username exists")]
        public async Task UpdateAsync_ShouldInvalidReturn_UserUsernameExists()
        {
            var request = UserMock.UpdateFullUserPasswordNullRequestMock();
            
            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Username.Equals(request.Username)))
                .ReturnsAsync(true);

            var result = await _userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Update user full Invalid validation")]
        public async Task UpdateAsync_ShouldInvalidReturn_Validation()
        {
            var request = UserMock.UpdateFullUserInvalidRequestMock();
            var userValidationInvalid = UserMock.UserValidatorErrorMock();
            var userService = new UserService(null, null, userValidationInvalid, null, null);

            var result = await userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors.Count > 0);
        }

        [TestMethod("Update user full Return exception")]
        public async Task UpdateAsync_ShouldProbrem_Exception()
        {
            var request = UserMock.UpdateFullUserRequestMock();
            var message = "Exception test";

            _ = _mockUserRepository
                .Setup(x => x.AnyAsync(c => c.Id != request.Id && c.Username.Equals(request.Username)))
                .Throws(new Exception(message));

            var result = await _userService.UpdateAsync(request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count > 0);
            Assert.IsNull(result.Data);
            Assert.IsTrue(result.Errors[0].Contains(message));
        }

        #endregion

        #region Login

        [TestMethod("Login Correct return")]
        public async Task LoginAsync_ShouldCorrectReturn_Login()
        {
            var user = UserMock.UserBasicMock();
            var userResponse = UserMock.UserActivedContributorMock();

            _ = _mockUserRepository
                .Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            _ = _mockMapper
                .Setup(x => x.Map<UserResponse>(user))
                .Returns(userResponse);

            var result = await _userService.LoginAsync<UserResponse>(It.IsAny<string>(), It.IsAny<string>());

            Assert.IsNotNull(result);
            Assert.AreEqual(userResponse, result);
            Assert.AreEqual(userResponse.Id, result.Id);
            Assert.AreEqual(userResponse.Actived, result.Actived);
            Assert.AreEqual(userResponse.Email, result.Email);
            Assert.AreEqual(userResponse.Name, result.Name);
            Assert.AreEqual(userResponse.Password, result.Password);
            Assert.AreEqual(userResponse.UserRoleId, result.UserRoleId);
            Assert.AreEqual(userResponse.RegistryDate, result.RegistryDate);
            Assert.AreEqual(userResponse.SecurityStamp, result.SecurityStamp);
            Assert.AreEqual(userResponse.Surname, result.Surname);
            Assert.AreEqual(userResponse.Username, result.Username);
        }

        [TestMethod("Login Null return")]
        public async Task LoginAsync_ShouldNullReturn_Login()
        {
            _ = _mockUserRepository
                .Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var result = await _userService.LoginAsync<UserResponse>(It.IsAny<string>(), It.IsAny<string>());

            Assert.IsNull(result);
        }

        [TestMethod("Login empty return")]
        public async Task LoginAsync_ShouldEmptyReturn_Login()
        {
            _ = _mockUserRepository
                .Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new User());

            var result = await _userService.LoginAsync<UserResponse>(It.IsAny<string>(), It.IsAny<string>());

            Assert.IsNull(result);
        }

        #endregion
    }
}