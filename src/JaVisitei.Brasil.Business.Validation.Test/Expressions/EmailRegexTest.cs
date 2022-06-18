using JaVisitei.Brasil.Business.Validation.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Validation.Test.Expressions
{
    [TestClass]
    public class EmailRegexaTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod("Return valid E-mail")]
        public void ValidateEmail_ExpresionIsValid_Sucsess()
        {
            var email = "teste@wton.com.br";

            var result = EmailRegex.ValidateEmail(email);

            Assert.IsTrue(result);
        }

        [TestMethod("Return invalid Email without at")]
        public void ValidateEmail_ExpresionIsInvalid_WithoutAt()
        {
            var email = "teste.com.br";

            var result = EmailRegex.ValidateEmail(email);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Email without dot")]
        public void ValidateEmail_ExpresionIsInvalid_WithoutDot()
        {
            var email = "teste@wtoncombr";

            var result = EmailRegex.ValidateEmail(email);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Email empty")]
        public void ValidateEmail_ExpresionIsInvalid_Empty()
        {
            var email = string.Empty;

            var result = EmailRegex.ValidateEmail(email);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Email exception")]
        public void ValidateEmail_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => EmailRegex.ValidateEmail(It.IsAny<string>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'email')");
        }
    }
}
