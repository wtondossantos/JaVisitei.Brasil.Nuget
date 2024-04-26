using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using JaVisitei.Brasil.Data.Entities;
using Moq;
using System;

namespace JaVisitei.Brasil.Security.Test
{
    [TestClass]
    public class TokenStringTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Environment.SetEnvironmentVariable("JWT_EXPIDED_MINUTE", "2");
            Environment.SetEnvironmentVariable("JWT_AUDIENCE", "audience");
            Environment.SetEnvironmentVariable("JWT_ISSUER", "issuer");
            Environment.SetEnvironmentVariable("JWT_KEY", "WE7BI5lcAW3jlmv35xTbfVbgGGWzgZ7Mo+fgJNSgVnk=");
            Environment.SetEnvironmentVariable("JWT_SUBJECT", "subject");
        }

        #region Generate authentication token

        [TestMethod("Return valid Generate authentication token")]
        public void GenerateAuthenticationToken_FormatIsValid_Sucsess()
        {
            var user = UserMock.UserContributorMock();

            var result = TokenString.GenerateAuthenticationToken(user);

            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result));
            
            var id = TokenString.ValidateJwtToken(result);

            Assert.IsNotNull(id);
            Assert.AreEqual(user.Id, id);
        }

        [TestMethod("Return invalid Generate authentication token nullable username")]
        public void GenerateAuthenticationToken_FormatIsInvalid_NullableUsername()
        {
            var user = UserMock.UserContributorMock();
            user.Username = null;

            var ex = Assert.ThrowsException<ArgumentNullException>(() => TokenString.GenerateAuthenticationToken(user));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'Username')");
        }

        [TestMethod("Return invalid Generate authentication token nullable userRole")]
        public void GenerateAuthenticationToken_FormatIsInvalid_NullableUserRole()
        {
            var user = UserMock.UserContributorMock();
            user.UserRole = null;

            var ex = Assert.ThrowsException<ArgumentNullException>(() => TokenString.GenerateAuthenticationToken(user));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'UserRole')");
        }

        [TestMethod("Return invalid Generate authentication token nullable userRole name")]
        public void GenerateAuthenticationToken_FormatIsInvalid_NullableUserRoleName()
        {
            var user = UserMock.UserContributorMock();
            user.UserRole.Name = null;

            var ex = Assert.ThrowsException<ArgumentNullException>(() => TokenString.GenerateAuthenticationToken(user));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'Name')");
        }

        [TestMethod("Return invalid Generate authentication token exception")]
        public void GenerateAuthenticationToken_FormatIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => TokenString.GenerateAuthenticationToken(It.IsAny<User>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'user')");
        }

        #endregion

        #region Generate authentication token

        [TestMethod("Return invalid Generate authentication token nullable email")]
        public void GenerateAuthenticationRefreshToken_FormatIsInvalid_NullableEmail()
        {
            var user = UserMock.UserContributorMock();
            user.Email = null;

            var ex = Assert.ThrowsException<ArgumentNullException>(() => TokenString.GenerateAuthenticationRefreshToken(user));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'Email')");
        }

        #endregion

        #region Generate email confirmation token X8

        [TestMethod("Return valid Generate email confirmation token X8")]
        public void RandomHexString_FormatIsValid_SucsessX8()
        {
            var alphacumeric = @"^[A-Za-z\d]{8}$";

            var result = TokenString.GenerateEmailConfirmationToken();

            Assert.IsTrue(result.Length.Equals(8));
            Assert.IsTrue(new Regex(alphacumeric).Match(result).Success);
        }

        #endregion

        #region Generate password reset token

        [TestMethod("Return valid Generate password reset token")]
        public void RandomAlphanumericString_FormatIsValid_Sucsess()
        {
            var length = 8;

            var result = TokenString.GeneratePasswordResetToken();

            Assert.IsTrue(result.Length.Equals(length));
        }

        #endregion
    }
}
