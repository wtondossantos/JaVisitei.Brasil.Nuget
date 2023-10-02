using JaVisitei.Brasil.Business.Validation.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Validation.Test.Expressions
{
    [TestClass]
    public class UsernameRegexTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod("Return valid Username")]
        public void ValidateUsername_ExpresionIsValid_Sucsess()
        {
            var username = "abcDEF_123";

            var result = UsernameRegex.ValidateUsername(username);

            Assert.IsTrue(result);
        }

        [TestMethod("Return valid Username without underline")]
        public void ValidateUsername_ExpresionIsValid_SucsessWithoutUnderline()
        {
            var username = "abcDEF123";

            var result = UsernameRegex.ValidateUsername(username);

            Assert.IsTrue(result);
        }

        [TestMethod("Return invalid Username with special character")]
        public void ValidateUsername_ExpresionIsInvalid_WithSpecialCharacter()
        {
            var username = "abcDEF@123";

            var result = UsernameRegex.ValidateUsername(username);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Username with space")]
        public void ValidateUsername_ExpresionIsInvalid_WithSpace()
        {
            var username = "abcDEF 123";

            var result = UsernameRegex.ValidateUsername(username);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Username minimum length")]
        public void ValidateUsername_ExpresionIsInvalid_MinimumLength()
        {
            var username = "Hi";

            var result = UsernameRegex.ValidateUsername(username);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Username maximum length")]
        public void ValidateUsername_ExpresionIsInvalid_MaximumLength()
        {
            var username = "Pneumonoultramicroscopicsilicovolcanoconiosis_Max50";

            var result = UsernameRegex.ValidateUsername(username);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Username empty")]
        public void ValidateUsername_ExpresionIsInvalid_Empty()
        {
            var username = string.Empty;

            var result = UsernameRegex.ValidateUsername(username);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Username exception")]
        public void ValidateUsername_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => UsernameRegex.ValidateUsername(It.IsAny<string>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'value')");
        }
    }
}
