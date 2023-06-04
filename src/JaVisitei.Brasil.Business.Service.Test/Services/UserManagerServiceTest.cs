using JaVisitei.Brasil.Business.Service.Services;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JaVisitei.Brasil.Business.ViewModels.Request.UserManager;
using JaVisitei.Brasil.Business.Service.Test.Mocks;
using JaVisitei.Brasil.Data.Entities;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Service.Test.Services
{
    [TestClass]
    public class UserManagerServiceTest
    {
        private readonly UserManagerService _userManagerService;
        private readonly Mock<IUserManagerRepository> _mockUserManagerRepository;
        private readonly Mock<IMapper> _mockMapper;

        public UserManagerServiceTest()
        {
            _mockUserManagerRepository = new Mock<IUserManagerRepository>();
            _mockMapper = new Mock<IMapper>();
            _userManagerService = new UserManagerService(_mockUserManagerRepository.Object, _mockMapper.Object);
        }

        #region Create user manager - e-mail confirmation

        [TestMethod("Create user manager Correct return")]
        public async Task CreateAsync_ShouldCorrectReturn_UserManagerCreated()
        {
            var mapper = UserManagerMock.ReturnUserManagerMapperMock();
            var response = UserManagerMock.ReturnUserManagerNotConfirmedMock();

            _ = _mockMapper
                .Setup(x => x.Map<UserManager>(It.IsAny<InsertEmailConfirmationUserManagerRequest>()))
                .Returns(mapper);

            _ = _mockUserManagerRepository
                .Setup(x => x.CreateAsync(mapper))
                .ReturnsAsync(response);

            var result = await _userManagerService.CreateAsync(It.IsAny<InsertEmailConfirmationUserManagerRequest>());

            Assert.IsNotNull(result);
            Assert.AreEqual(response, result);
            Assert.AreEqual(response.UserId, result.UserId);
        }

        [TestMethod("Create user manager Correct return nullable")]
        public async Task CreateAsync_ShouldNullReturn_Nullable()
        {
            var request = UserManagerMock.InsertEmailConfirmationUserManagerRequestMock();
            var mapper = UserManagerMock.ReturnUserManagerMapperMock();
            
            _ = _mockMapper
                .Setup(x => x.Map<UserManager>(It.IsAny<InsertEmailConfirmationUserManagerRequest>()))
                .Returns(mapper);

            _ = _mockUserManagerRepository
                .Setup(x => x.CreateAsync(mapper))
                .ReturnsAsync((UserManager)null);

            var result = await _userManagerService.CreateEmailConfirmationAsync(It.IsAny<string>());

            Assert.IsNull(result);
        }

        #endregion

        #region Create user manager e-mail confirmation

        [TestMethod("Create user manager e-mail confirmation Correct return")]
        public async Task CreateEmailConfirmationAsync_ShouldCorrectReturn_EmailConfirmationUserManagerCreated()
        {
            var mapper = UserManagerMock.ReturnUserManagerMapperMock();
            var response = UserManagerMock.ReturnUserManagerNotConfirmedMock();

            _ = _mockMapper
                .Setup(x => x.Map<UserManager>(It.IsAny<InsertEmailConfirmationUserManagerRequest>()))
                .Returns(mapper);

            _ = _mockUserManagerRepository
                .Setup(x => x.CreateAsync(mapper))
                .ReturnsAsync(response);

            var result = await _userManagerService.CreateEmailConfirmationAsync(It.IsAny<string>());

            Assert.IsNotNull(result);
            Assert.AreEqual(response, result);
            Assert.AreEqual(response.UserId, result.UserId);
        }

        [TestMethod("Create user manager e-mail confirmation Correct return nullable")]
        public async Task CreateEmailConfirmationAsync_ShouldNullReturn_EmailConfirmationNullable()
        {
            var request = UserManagerMock.InsertEmailConfirmationUserManagerRequestMock();
            var mapper = UserManagerMock.ReturnUserManagerMapperMock();

            _ = _mockMapper
                .Setup(x => x.Map<UserManager>(It.IsAny<InsertEmailConfirmationUserManagerRequest>()))
                .Returns(mapper);

            _ = _mockUserManagerRepository
                .Setup(x => x.CreateAsync(mapper))
                .ReturnsAsync((UserManager)null);

            var result = await _userManagerService.CreateEmailConfirmationAsync(It.IsAny<string>());

            Assert.IsNull(result);
        }

        #endregion

        #region Create user manager - password reset

        [TestMethod("Create user manager password reset Correct return")]
        public async Task CreateAsync_ShouldCorrectReturn_PasswordResetUserManagerCreated()
        {
            var mapper = UserManagerMock.ReturnUserManagerMapperMock();
            var response = UserManagerMock.ReturnUserManagerNotConfirmedMock();

            _ = _mockMapper
                .Setup(x => x.Map<UserManager>(It.IsAny<InsertPasswordResetUserManagerRequest>()))
                .Returns(mapper);

            _ = _mockUserManagerRepository
                .Setup(x => x.CreateAsync(mapper))
                .ReturnsAsync(response);

            var result = await _userManagerService.CreateAsync(It.IsAny<InsertPasswordResetUserManagerRequest>());

            Assert.IsNotNull(result);
            Assert.AreEqual(response, result);
            Assert.AreEqual(response.UserId, result.UserId);
        }

        [TestMethod("Create user manager password reset Correct return nullable")]
        public async Task CreateAsync_ShouldNullReturn_PasswordResetNullable()
        {
            var request = UserManagerMock.InsertEmailConfirmationUserManagerRequestMock();
            var mapper = UserManagerMock.ReturnUserManagerMapperMock();

            _ = _mockMapper
                .Setup(x => x.Map<UserManager>(It.IsAny<InsertPasswordResetUserManagerRequest>()))
                .Returns(mapper);

            _ = _mockUserManagerRepository
                .Setup(x => x.CreateAsync(mapper))
                .ReturnsAsync((UserManager)null);

            var result = await _userManagerService.CreateAsync(It.IsAny<InsertPasswordResetUserManagerRequest>());

            Assert.IsNull(result);
        }

        #endregion

        #region Create user manager password reset

        [TestMethod("Create user manager password reset Correct return")]
        public async Task CreatePasswordResetAsync_ShouldCorrectReturn_UserManagerCreated()
        {
            var mapper = UserManagerMock.ReturnUserManagerMapperMock();
            var response = UserManagerMock.ReturnUserManagerNotConfirmedMock();

            _ = _mockMapper
                .Setup(x => x.Map<UserManager>(It.IsAny<InsertPasswordResetUserManagerRequest>()))
                .Returns(mapper);

            _ = _mockUserManagerRepository
                .Setup(x => x.CreateAsync(mapper))
                .ReturnsAsync(response);

            var result = await _userManagerService.CreatePasswordResetAsync(It.IsAny<string>());

            Assert.IsNotNull(result);
            Assert.AreEqual(response, result);
            Assert.AreEqual(response.UserId, result.UserId);
        }

        [TestMethod("Create user manager password reset Correct return nullable")]
        public async Task CreatePasswordResetAsync_ShouldNullReturn_Nullable()
        {
            var request = UserManagerMock.InsertEmailConfirmationUserManagerRequestMock();
            var mapper = UserManagerMock.ReturnUserManagerMapperMock();

            _ = _mockMapper
                .Setup(x => x.Map<UserManager>(It.IsAny<InsertPasswordResetUserManagerRequest>()))
                .Returns(mapper);

            _ = _mockUserManagerRepository
                .Setup(x => x.CreateAsync(mapper))
                .ReturnsAsync((UserManager)null);

            var result = await _userManagerService.CreatePasswordResetAsync(It.IsAny<string>());

            Assert.IsNull(result);
        }

        #endregion

        #region User manager by manager code

        [TestMethod("User manager by manager code Correct return")]
        public async Task GetByManagerCodeAsync_ShouldCorrectReturn_UserManager()
        {
            var managerCode = "ABC123AB99";
            var response = UserManagerMock.ReturnUserManagerNotConfirmedMock();

            _ = _mockUserManagerRepository
                .Setup(x => x.GetFirstOrDefaultAsync(x => x.ManagerCode.Equals(managerCode.Substring(0, 8)) &&
                       x.Id.Equals(Convert.ToInt32(managerCode.Substring(8, managerCode.Length - 8))), null))
                .ReturnsAsync(response);

            var result = await _userManagerService.GetByManagerCodeAsync(managerCode);

            Assert.IsNotNull(result);
            Assert.AreEqual(managerCode, result.ManagerCode);
        }

        [TestMethod("User manager by manager code Null return")]
        public async Task GetByManagerCodeAsync_ShouldNullReturn_UserManager()
        {
            var managerCode = "ABC123AB99";

            _ = _mockUserManagerRepository
                .Setup(x => x.GetFirstOrDefaultAsync(x => x.ManagerCode.Equals(managerCode.Substring(0, 8)) &&
                       x.Id.Equals(Convert.ToInt32(managerCode.Substring(8, managerCode.Length - 8))), null))
                .ReturnsAsync((UserManager)null);

            var result = await _userManagerService.GetByManagerCodeAsync(managerCode);

            Assert.IsNull(result);
        }

        [TestMethod("User manager by manager code Null return user manager empty")]
        public async Task GetByManagerCodeAsync_ShouldNullReturn_UserManagerUserManagerEmpty()
        {
            var result = await _userManagerService.GetByManagerCodeAsync(It.IsAny<string>());

            Assert.IsNull(result);
        }

        #endregion

        #region Confirmed change

        [TestMethod("Confirmed change Correct return")]
        public async Task ConfirmedChangeAsync_ShouldCorrectReturn_UserManager()
        {
            var userManager = UserManagerMock.ReturnUserManagerNotConfirmedMock();

            _ = _mockUserManagerRepository
                .Setup(x => x.UpdateAsync(userManager))
                .ReturnsAsync(true);

            var result = await _userManagerService.ConfirmedChangeAsync(userManager);

            Assert.IsNotNull(result);
            Assert.AreEqual(userManager.ConfirmedChange, result);
        }

        [TestMethod("Confirmed change Return false")]
        public async Task ConfirmedChangeAsync_ShouldReturnFalse_UserManager()
        {
            var userManager = UserManagerMock.ReturnUserManagerNotConfirmedMock();

            _ = _mockUserManagerRepository
                .Setup(x => x.UpdateAsync(userManager))
                .ReturnsAsync(false);

            var result = await _userManagerService.ConfirmedChangeAsync(userManager);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(userManager.ConfirmedChange, result);
        }

        [TestMethod("Confirmed change Return false User manager nullable")]
        public async Task ConfirmedChangeAsync_ShouldReturnNull_Nullable()
        {
            var result = await _userManagerService.ConfirmedChangeAsync(It.IsAny<UserManager>());

            Assert.IsNotNull(result);
            Assert.AreEqual(false, result);
        }

        [TestMethod("Confirmed change Return false User manager id zero")]
        public async Task ConfirmedChangeAsync_ShouldInvalidReturn_UserManagerId()
        {
            var result = await _userManagerService.ConfirmedChangeAsync(new UserManager());

            Assert.IsNotNull(result);
            Assert.AreEqual(false, result);
        }

        #endregion
    }
}
