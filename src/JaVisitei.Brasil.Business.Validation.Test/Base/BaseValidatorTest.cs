using JaVisitei.Brasil.Business.Validation.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Validation.Test.Base
{
    [TestClass]
    public class BaseValidatorTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod("Return valid Regex validator")]
        public void RegexValidade_ExpresionIsValid_Sucsess()
        {
            var expression = @"^[\d]+$";
            var value = "1";

            var result = BaseValidator.RegexValidade(value, expression);

            Assert.IsTrue(result);
        }

        [TestMethod("Return valid Regex validator negative")]
        public void RegexValidade_ExpresionIsValid_SucsessNegative()
        {
            var expression = @"^[\d]+$";
            var value = "a";

            var result = BaseValidator.RegexValidade(value, expression);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Validator without value")]
        public void RegexValidade_ExpresionIsInvalid_WithoutValue()
        {
            var expression = @"^[\d]+$";
            var value = string.Empty;

            var result = BaseValidator.RegexValidade(value, expression);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Validator without expression")]
        public void RegexValidade_ExpresionIsInvalid_ExceptionExpression()
        {
            var expression = string.Empty;
            var value = "1";

            var ex = Assert.ThrowsException<ArgumentNullException>(() => BaseValidator.RegexValidade(value, expression));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'expression')");
        }

        [TestMethod("Return invalid Validator without value nullable")]
        public void RegexValidade_ExpresionIsInvalid_ExceptionValueNullable()
        {
            var expression = @"^[\d]+$";

            var ex = Assert.ThrowsException<ArgumentNullException>(() => BaseValidator.RegexValidade(It.IsAny<string>(), expression));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'value')");
        }
    }
}
