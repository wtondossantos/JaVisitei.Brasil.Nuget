using JaVisitei.Brasil.Business.Validation.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Validation.Test.Expressions
{
    [TestClass]
    public class OnlyNumberRegexTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod("Return valid Only number")]
        public void ValidateOnlyNumber_ExpresionIsValid_Sucsess()
        {
            var number = "123456789";

            var result = OnlyNumberRegex.ValidateOnlyNumber(number);

            Assert.IsTrue(result);
        }

        [TestMethod("Return valid Number with space")]
        public void ValidateOnlyNumber_ExpresionIsValid_NumberWithSpace()
        {
            var number = "12345 6789";

            var result = OnlyNumberRegex.ValidateOnlyNumber(number);

            Assert.IsTrue(result);
        }

        [TestMethod("Return invalid Number with text")]
        public void ValidateOnlyNumber_ExpresionIsInvalid_NumberWithText()
        {
            var number = "NumberWithText123";

            var result = OnlyNumberRegex.ValidateOnlyNumber(number);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Number with special caracter")]
        public void ValidateOnlyNumber_ExpresionIsInvalid_NumberWithSpecialCaracter()
        {
            var number = "123&$";

            var result = OnlyNumberRegex.ValidateOnlyNumber(number);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Number empty")]
        public void ValidateOnlyNumber_ExpresionIsInvalid_NumberWithEmpty()
        {
            var number = string.Empty;

            var result = OnlyNumberRegex.ValidateOnlyNumber(number);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Number exception")]
        public void ValidateOnlyNumber_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => OnlyNumberRegex.ValidateOnlyNumber(It.IsAny<string>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'value')");
        }
    }
}
