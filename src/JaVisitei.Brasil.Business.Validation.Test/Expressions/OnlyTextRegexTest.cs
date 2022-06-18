using JaVisitei.Brasil.Business.Validation.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Validation.Test.Expressions
{
    [TestClass]
    public class OnlyTextRegexTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod("Return valid Only text")]
        public void ValidateOnlyText_ExpresionIsValid_Sucsess()
        {
            var text = "Only text with space";

            var result = OnlyTextRegex.ValidateOnlyText(text);

            Assert.IsTrue(result);
        }

        [TestMethod("Return invalid Text with number")]
        public void ValidateOnlyText_ExpresionIsInvalid_TextWithNumber()
        {
            var text = "Text with 123";

            var result = OnlyTextRegex.ValidateOnlyText(text);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Text with special caracter")]
        public void ValidateOnlyText_ExpresionIsInvalid_TextWithSpecialCaracter()
        {
            var text = "Text with &$";

            var result = OnlyTextRegex.ValidateOnlyText(text);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Text empty")]
        public void ValidateOnlyText_ExpresionIsInvalid_TextWithEmpty()
        {
            var text = string.Empty;

            var result = OnlyTextRegex.ValidateOnlyText(text);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Text exception")]
        public void ValidateOnlyText_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => OnlyTextRegex.ValidateOnlyText(It.IsAny<string>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'value')");
        }
    }
}
