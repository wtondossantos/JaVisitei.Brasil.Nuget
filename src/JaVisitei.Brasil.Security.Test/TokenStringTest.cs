using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using JaVisitei.Brasil.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Security.Claims;
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
            Environment.SetEnvironmentVariable("JWT_KEY", "teste@teste.com.zz");
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

            var jsonToken = new JwtSecurityTokenHandler().ReadToken(result);
            var tokenS = jsonToken as JwtSecurityToken;
            
            Assert.IsNotNull(tokenS);

            var claims = tokenS.Claims as List<Claim>;

            Assert.IsNotNull(claims);
            Assert.AreEqual(Environment.GetEnvironmentVariable("JWT_SUBJECT"), claims[0].Value);
            Assert.AreEqual(Environment.GetEnvironmentVariable("JWT_SUBJECT"), tokenS.Subject);
            Assert.IsFalse(string.IsNullOrEmpty(claims[1].Value));
            Assert.IsFalse(string.IsNullOrEmpty(claims[2].Value));
            Assert.AreEqual(user.Id.ToString(), claims[3].Value);
            Assert.AreEqual(user.Username, claims[4].Value);
            Assert.AreEqual(user.Email, claims[5].Value);
            Assert.AreEqual(Environment.GetEnvironmentVariable("JWT_KEY"), claims[5].Value);
            Assert.AreEqual(user.UserRole.Name, claims[6].Value);
            Assert.IsFalse(string.IsNullOrEmpty(claims[7].Value));
            Assert.AreEqual(Environment.GetEnvironmentVariable("JWT_ISSUER"), claims[8].Value);
            Assert.AreEqual(Environment.GetEnvironmentVariable("JWT_AUDIENCE"), claims[9].Value);

            var audiences = tokenS.Audiences as List<string>;

            Assert.IsNotNull(audiences);
            Assert.AreEqual(Environment.GetEnvironmentVariable("JWT_AUDIENCE"), audiences[0]);
        }

        [TestMethod("Return invalid Generate authentication token nullable username")]
        public void GenerateAuthenticationToken_FormatIsInvalid_NullableUsername()
        {
            var user = UserMock.UserContributorMock();
            user.Username = null;

            var ex = Assert.ThrowsException<ArgumentNullException>(() => TokenString.GenerateAuthenticationToken(user));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'Username')");
        }

        [TestMethod("Return invalid Generate authentication token nullable email")]
        public void GenerateAuthenticationToken_FormatIsInvalid_NullableEmail()
        {
            var user = UserMock.UserContributorMock();
            user.Email = null;

            var ex = Assert.ThrowsException<ArgumentNullException>(() => TokenString.GenerateAuthenticationToken(user));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'Email')");
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
