using JaVisitei.Brasil.Business.Validation.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Validation.Test.Expressions
{
    [TestClass]
    public class PasswordRegexTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod("Return valid Password")]
        public void ValidatePassword_ExpresionIsValid_Sucsess()
        {
            var password = "Abcd;',2";

            var result = PasswordRegex.ValidatePassword(password);

            Assert.IsTrue(result);
        }

        [TestMethod("Return valid Password without special character")]
        public void ValidatePassword_ExpresionIsValid_WithoutSpecialCharacter()
        {
            var password = "Abcd1234";

            var result = PasswordRegex.ValidatePassword(password);

            Assert.IsTrue(result);
        }

        [TestMethod("Return valid Password with space")]
        public void ValidatePassword_ExpresionIsValid_WithSpace()
        {
            var password = "abcDEF 123";

            var result = PasswordRegex.ValidatePassword(password);

            Assert.IsTrue(result);
        }

        [TestMethod("Return valid Password without number")]
        public void ValidatePassword_ExpresionIsValid_WithoutNumber()
        {
            var password = "Abcd;',h";

            var result = PasswordRegex.ValidatePassword(password);

            Assert.IsFalse(result);
        }

        [TestMethod("Return valid Password without uppercase")]
        public void ValidatePassword_ExpresionIsValid_WithoutUppercase()
        {
            var password = "abcd;',2";

            var result = PasswordRegex.ValidatePassword(password);

            Assert.IsFalse(result);
        }

        [TestMethod("Return valid Password without lowercase")]
        public void ValidatePassword_ExpresionIsValid_WithoutLowercase()
        {
            var password = "ABCD;',2";

            var result = PasswordRegex.ValidatePassword(password);

            Assert.IsFalse(result);
        }

        [TestMethod("Return valid Password without letter")]
        public void ValidatePassword_ExpresionIsValid_WithoutLetter()
        {
            var password = "1234;',8";

            var result = PasswordRegex.ValidatePassword(password);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Password minimum length")]
        public void ValidatePassword_ExpresionIsInvalid_MinimumLength()
        {
            var password = "Seven@7";

            var result = PasswordRegex.ValidatePassword(password);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Password maximum length")]
        public void ValidatePassword_ExpresionIsInvalid_MaximumLength()
        {
            var password = "PneumonoultramicroscopicsilicovolcanoconiosisPneumonoultramicroscopicsilicovolcanoconiosisPneumonoultramicroscopicsilicovolcanoconiosisPneumonoultramicroscopicsilicovolcanoconiosisPneumonoultramicroscopicsilicovolcanoconiosisPneumonoultramicroscopicsil@256";

            var result = PasswordRegex.ValidatePassword(password);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Password empty")]
        public void ValidatePassword_ExpresionIsInvalid_Empty()
        {
            var password = string.Empty;

            var result = PasswordRegex.ValidatePassword(password);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Password exception")]
        public void ValidatePassword_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => PasswordRegex.ValidatePassword(It.IsAny<string>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'value')");
        }
    }
}
