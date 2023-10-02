using JaVisitei.Brasil.Business.Validation.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Validation.Test.Expressions
{
    [TestClass]
    public class ManagerCodeRegexTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        #region Manager code full

        [TestMethod("Return valid ManagerCode")]
        public void ValidateManagerCodeFull_ExpresionIsValid_Sucsess()
        {
            var code = "SDf4ASD412345";

            var result = ManagerCodeRegex.ValidateManagerCodeFull(code);

            Assert.IsTrue(result);
        }

        [TestMethod("Return invalid ManagerCode with special character")]
        public void ValidateManagerCodeFull_ExpresionIsInvalid_WithSpecialCharacter()
        {
            var code = "SDf4ASD@412345";

            var result = ManagerCodeRegex.ValidateManagerCodeFull(code);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ManagerCode with space")]
        public void ValidateManagerCodeFull_ExpresionIsInvalid_WithSpace()
        {
            var code = "SDf4ASD 345";

            var result = ManagerCodeRegex.ValidateManagerCodeFull(code);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ManagerCode minimum length")]
        public void ValidateManagerCodeFull_ExpresionIsInvalid_MinimumLength()
        {
            var code = "1AASDF449";

            var result = ManagerCodeRegex.ValidateManagerCodeFull(code);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ManagerCode empty")]
        public void ValidateManagerCodeFull_ExpresionIsInvalid_Empty()
        {
            var code = string.Empty;

            var result = ManagerCodeRegex.ValidateManagerCodeFull(code);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ManagerCode exception")]
        public void ValidateManagerCodeFull_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => ManagerCodeRegex.ValidateManagerCodeFull(It.IsAny<string>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'value')");
        }

        #endregion

        #region Manager code

        [TestMethod("Return valid ManagerCode")]
        public void ValidateManagerCode_ExpresionIsValid_Sucsess()
        {
            var code = "SDf4ASD4";

            var result = ManagerCodeRegex.ValidateManagerCode(code);

            Assert.IsTrue(result);
        }

        [TestMethod("Return invalid ManagerCode with special character")]
        public void ValidateManagerCode_ExpresionIsInvalid_WithSpecialCharacter()
        {
            var code = "SDf4ASD@";

            var result = ManagerCodeRegex.ValidateManagerCode(code);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ManagerCode with space")]
        public void ValidateManagerCode_ExpresionIsInvalid_WithSpace()
        {
            var code = "SDf4 345";

            var result = ManagerCodeRegex.ValidateManagerCode(code);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ManagerCode minimum length")]
        public void ValidateManagerCode_ExpresionIsInvalid_MinimumLength()
        {
            var code = "ASDF449";

            var result = ManagerCodeRegex.ValidateManagerCode(code);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ManagerCode empty")]
        public void ValidateManagerCode_ExpresionIsInvalid_Empty()
        {
            var code = string.Empty;

            var result = ManagerCodeRegex.ValidateManagerCode(code);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ManagerCode exception")]
        public void ValidateManagerCode_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => ManagerCodeRegex.ValidateManagerCode(It.IsAny<string>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'value')");
        }

        #endregion
    }
}
