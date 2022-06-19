using JaVisitei.Brasil.Business.Validation.Test.Mocks;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Validation.Test.Validators
{
    [TestClass]
    public class UserValidatorTest
    {
        private readonly UserValidator _userValidator;

        public UserValidatorTest()
        {
            _userValidator = new UserValidator();
        }

        #region User creation

        [TestMethod("User creation Correct return")]
        public void ValidatesUserCreation_ShouldCorrectReturn_Validation()
        {
            var request = UserMock.CreateUserRequestMock();

            _userValidator.ValidatesUserCreation(request);

            Assert.IsTrue(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(0));
        }

        [TestMethod("User creation Invalid return")]
        public void ValidatesUserCreation_ShouldInvalidReturn_Validation()
        {
            var request = UserMock.CreateUserInvalidRequestMock();

            _userValidator.ValidatesUserCreation(request);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(6));
        }

        [TestMethod("User creation Invalid return e-mail and password")]
        public void ValidatesUserCreation_ShouldInvalidReturn_ValidationEmailAndPassword()
        {
            var request = UserMock.CreateUserInvalidEmailAndPasswordRequestMock();

            _userValidator.ValidatesUserCreation(request);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(2));
        }
        
        [TestMethod("User creation Invalid return max length")]
        public void ValidatesUserCreation_ShouldInvalidReturn_ValidationMaxLength()
        {
            var request = UserMock.CreateUserInvalidMaxLengthRequestMock();

            _userValidator.ValidatesUserCreation(request);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(6));
        }

        [TestMethod("User creation Invalid return min length")]
        public void ValidatesUserCreation_ShouldInvalidReturn_ValidationMinLength()
        {
            var request = UserMock.CreateUserInvalidMinLengthRequestMock();

            _userValidator.ValidatesUserCreation(request);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(5));
        }

        [TestMethod("User creation Invalid return empty")]
        public void ValidatesUserCreation_ShouldInvalidReturn_Empty()
        {
            var request = UserMock.CreateUserEmptyRequestMock();

            _userValidator.ValidatesUserCreation(request);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(5));
        }

        [TestMethod("User creation Invalid return nullable")]
        public void ValidatesUserCreation_ShouldInvalidReturn_Nulable()
        {
            _userValidator.ValidatesUserCreation(new InsertFullUserRequest());

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(5));
        }

        [TestMethod("User creation Invalid return excpetion")]
        public void ValidatesUserCreation_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _userValidator.ValidatesUserCreation(It.IsAny<InsertFullUserRequest>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'request')");
        }

        #endregion

        #region User Edition

        [TestMethod("User edition Correct return")]
        public void ValidatesUserEdition_ShouldCorrectReturn_Validation()
        {
            var request = UserMock.UpdateUserRequestMock();

            _userValidator.ValidatesUserEdition(request);

            Assert.IsTrue(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(0));
        }

        [TestMethod("User edition Invalid return")]
        public void ValidatesUserEdition_ShouldInvalidReturn_Validation()
        {
            var request = UserMock.UpdateUserInvalidRequestMock();

            _userValidator.ValidatesUserEdition(request);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(6));
        }

        [TestMethod("User edition Invalid return e-mail and password")]
        public void ValidatesUserEdition_ShouldInvalidReturn_ValidationEmailAndPassword()
        {
            var request = UserMock.UpdateUserInvalidEmailAndPasswordRequestMock();

            _userValidator.ValidatesUserEdition(request);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(2));
        }

        [TestMethod("User edition Invalid return max length")]
        public void ValidatesUserEdition_ShouldInvalidReturn_ValidationMaxLength()
        {
            var request = UserMock.UpdateUserInvalidMaxLengthRequestMock();

            _userValidator.ValidatesUserEdition(request);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(5));
        }

        [TestMethod("User edition Invalid return min length")]
        public void ValidatesUserEdition_ShouldInvalidReturn_ValidationMinLength()
        {
            var request = UserMock.UpdateUserInvalidMinLengthRequestMock();

            _userValidator.ValidatesUserEdition(request);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(6));
        }

        [TestMethod("User edition Invalid return empty")]
        public void ValidatesUserEdition_ShouldInvalidReturn_Empty()
        {
            var request = UserMock.UpdateUserEmptyRequestMock();

            _userValidator.ValidatesUserEdition(request);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(6));
        }

        [TestMethod("User edition Invalid return nullable")]
        public void ValidatesUserEdition_ShouldInvalidReturn_Nulable()
        {
            _userValidator.ValidatesUserEdition(new UpdateFullUserRequest());

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(4));
        }

        [TestMethod("User edition Invalid return excpetion")]
        public void ValidatesUserEdition_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _userValidator.ValidatesUserEdition(It.IsAny<UpdateFullUserRequest>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'request')");
        }

        #endregion

        #region Name

        [TestMethod("Name Correct return")]
        public void ValidatesUserName_ShouldCorrectReturn_Validation()
        {
            var request = UserMock.UpdateUserRequestMock();

            _userValidator.ValidatesUserName(request.Name);

            Assert.IsTrue(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Name Invalid return")]
        public void ValidatesUserName_ShouldInvalidReturn_Validation()
        {
            var request = UserMock.UpdateUserInvalidRequestMock();

            _userValidator.ValidatesUserName(request.Name);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Name Invalid return max length")]
        public void ValidatesUserName_ShouldInvalidReturn_ValidationMaxLength()
        {
            var request = UserMock.UpdateUserInvalidMaxLengthRequestMock();

            _userValidator.ValidatesUserName(request.Name);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Name Invalid return min length")]
        public void ValidatesUserName_ShouldInvalidReturn_ValidationMinLength()
        {
            var request = UserMock.UpdateUserInvalidMinLengthRequestMock();

            _userValidator.ValidatesUserName(request.Name);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Name Invalid return empty")]
        public void ValidatesUserName_ShouldInvalidReturn_Empty()
        {
            var name = string.Empty;

            _userValidator.ValidatesUserName(name);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Name Invalid return nullable")]
        public void ValidatesUserName_ShouldInvalidReturn_Nulable()
        {
            _userValidator.ValidatesUserName(It.IsAny<string>());

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        #endregion

        #region Surname

        [TestMethod("Surname Correct return")]
        public void ValidatesUserSurname_ShouldCorrectReturn_Validation()
        {
            var request = UserMock.UpdateUserRequestMock();

            _userValidator.ValidatesUserSurname(request.Surname);

            Assert.IsTrue(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Surname Invalid return")]
        public void ValidatesUserSurname_ShouldInvalidReturn_Validation()
        {
            var request = UserMock.UpdateUserInvalidRequestMock();

            _userValidator.ValidatesUserSurname(request.Surname);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Surname Invalid return max length")]
        public void ValidatesUserSurname_ShouldInvalidReturn_ValidationMaxLength()
        {
            var request = UserMock.UpdateUserInvalidMaxLengthRequestMock();

            _userValidator.ValidatesUserSurname(request.Surname);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        #endregion

        #region Username

        [TestMethod("Username Correct return")]
        public void ValidatesUserUsername_ShouldCorrectReturn_Validation()
        {
            var request = UserMock.UpdateUserRequestMock();

            _userValidator.ValidatesUserUsername(request.Username);

            Assert.IsTrue(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Username Invalid return")]
        public void ValidatesUserUsername_ShouldInvalidReturn_Validation()
        {
            var request = UserMock.UpdateUserInvalidRequestMock();

            _userValidator.ValidatesUserUsername(request.Username);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Username Invalid return max length")]
        public void ValidatesUserUsername_ShouldInvalidReturn_ValidationMaxLength()
        {
            var request = UserMock.UpdateUserInvalidMaxLengthRequestMock();

            _userValidator.ValidatesUserUsername(request.Username);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Username Invalid return min length")]
        public void ValidatesUserUsername_ShouldInvalidReturn_ValidationMinLength()
        {
            var request = UserMock.UpdateUserInvalidMinLengthRequestMock();

            _userValidator.ValidatesUserUsername(request.Username);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Username Invalid return empty")]
        public void ValidatesUserUsername_ShouldInvalidReturn_Empty()
        {
            var Username = string.Empty;

            _userValidator.ValidatesUserUsername(Username);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Username Invalid return nullable")]
        public void ValidatesUserUsername_ShouldInvalidReturn_Nulable()
        {
            _userValidator.ValidatesUserUsername(It.IsAny<string>());

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        #endregion

        #region Password

        [TestMethod("Email Correct return")]
        public void ValidatesUserEmail_ShouldCorrectReturn_Validation()
        {
            var request = UserMock.UpdateUserRequestMock();

            _userValidator.ValidatesUserEmail(request.Email, request.ReEmail);

            Assert.IsTrue(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Email Invalid return")]
        public void ValidatesUserEmail_ShouldInvalidReturn_Validation()
        {
            var request = UserMock.UpdateUserInvalidRequestMock();

            _userValidator.ValidatesUserEmail(request.Email, request.ReEmail);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Email Invalid return max length")]
        public void ValidatesUserEmail_ShouldInvalidReturn_ValidationMaxLength()
        {
            var request = UserMock.UpdateUserInvalidMaxLengthRequestMock();

            _userValidator.ValidatesUserEmail(request.Email, request.ReEmail);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Email Invalid return min length")]
        public void ValidatesUserEmail_ShouldInvalidReturn_ValidationMinLength()
        {
            var request = UserMock.UpdateUserInvalidMinLengthRequestMock();

            _userValidator.ValidatesUserEmail(request.Email, request.ReEmail);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Email Invalid return empty")]
        public void ValidatesUserEmail_ShouldInvalidReturn_Empty()
        {
            var email = string.Empty;
            var reEmail = string.Empty;

            _userValidator.ValidatesUserEmail(email, reEmail);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Email Invalid return compare")]
        public void ValidatesUserEmail_ShouldInvalidReturn_Compare()
        {
            var request = UserMock.UpdateUserInvalidEmailAndPasswordRequestMock();

            _userValidator.ValidatesUserEmail(request.Email, request.ReEmail);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Email Invalid return nullable")]
        public void ValidatesUserEmail_ShouldInvalidReturn_Nulable()
        {
            _userValidator.ValidatesUserEmail(It.IsAny<string>(), It.IsAny<string>());

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        #endregion

        #region Password

        [TestMethod("Password Correct return")]
        public void ValidatesUserPassword_ShouldCorrectReturn_Validation()
        {
            var request = UserMock.UpdateUserRequestMock();

            _userValidator.ValidatesUserPassword(request.Password, request.RePassword);

            Assert.IsTrue(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Password Invalid return")]
        public void ValidatesUserPassword_ShouldInvalidReturn_Validation()
        {
            var request = UserMock.UpdateUserInvalidRequestMock();

            _userValidator.ValidatesUserPassword(request.Password, request.RePassword);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Password Invalid return max length")]
        public void ValidatesUserPassword_ShouldInvalidReturn_ValidationMaxLength()
        {
            var request = UserMock.UpdateUserInvalidMaxLengthRequestMock();

            _userValidator.ValidatesUserPassword(request.Password, request.RePassword);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Password Invalid return min length")]
        public void ValidatesUserPassword_ShouldInvalidReturn_ValidationMinLength()
        {
            var request = UserMock.UpdateUserInvalidMinLengthRequestMock();

            _userValidator.ValidatesUserPassword(request.Password, request.RePassword);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Password Invalid return empty")]
        public void ValidatesUserPassword_ShouldInvalidReturn_Empty()
        {
            var Password = string.Empty;
            var rePassword = string.Empty;

            _userValidator.ValidatesUserPassword(Password, rePassword);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Password Invalid return compare")]
        public void ValidatesUserPassword_ShouldInvalidReturn_Compare()
        {
            var request = UserMock.UpdateUserInvalidEmailAndPasswordRequestMock();

            _userValidator.ValidatesUserPassword(request.Password, request.RePassword);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        [TestMethod("Password Invalid return nullable")]
        public void ValidatesUserPassword_ShouldInvalidReturn_Nulable()
        {
            _userValidator.ValidatesUserPassword(It.IsAny<string>(), It.IsAny<string>());

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        #endregion

        #region User role id

        [TestMethod("User role id Correct return")]
        public void ValidatesUserRoleId_ShouldCorrectReturn_Validation()
        {
            var request = UserMock.UpdateUserRequestMock();

            _userValidator.ValidatesUserRoleId(request.UserRoleId);

            Assert.IsTrue(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(0));
        }

        [TestMethod("User role id Invalid return")]
        public void ValidatesUserRoleId_ShouldInvalidReturn_Validation()
        {
            var request = UserMock.UpdateUserInvalidRequestMock();

            _userValidator.ValidatesUserRoleId(request.UserRoleId);

            Assert.IsFalse(_userValidator.IsValid);
            Assert.IsTrue(_userValidator.Errors.Count.Equals(1));
        }

        #endregion
    }
}