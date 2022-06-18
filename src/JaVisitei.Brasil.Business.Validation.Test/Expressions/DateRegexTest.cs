using JaVisitei.Brasil.Business.Validation.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Validation.Test.Expressions
{
    [TestClass]
    public class DateRegexTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod("Return valid Date dd/MM/yyyy")]
        public void ValidateDate_ExpresionIsValid_SucsessWidthSlash()
        {
            var date = "29/02/2000";

            var result = DateRegex.ValidateDate(date);

            Assert.IsTrue(result);
        }

        [TestMethod("Return valid Date dd-MM-yyyy")]
        public void ValidateDate_ExpresionIsValid_SucsessWidthHyphen()
        {
            var date = "29-02-2000";

            var result = DateRegex.ValidateDate(date);

            Assert.IsTrue(result);
        }

        [TestMethod("Return invalid Date without partition")]
        public void ValidateDate_ExpresionIsInvalid_WithoutPartition()
        {
            var date = "29 02 2000";

            var result = DateRegex.ValidateDate(date);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Date without year")]
        public void ValidateDate_ExpresionIsInvalid_WithoutYear()
        {
            var date = "29-02";

            var result = DateRegex.ValidateDate(date);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Date format incorrect")]
        public void ValidateDate_ExpresionIsInvalid_FormatIncorrect()
        {
            var date = "29-02-22";

            var result = DateRegex.ValidateDate(date);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Date order incorrect")]
        public void ValidateDate_ExpresionIsInvalid_OrderIncorrect()
        {
            var date = "2000-02-29";

            var result = DateRegex.ValidateDate(date);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Date empty")]
        public void ValidateDate_ExpresionIsInvalid_Empty()
        {
            var date = string.Empty;

            var result = DateRegex.ValidateDate(date);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid Date exception")]
        public void ValidateDate_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => DateRegex.ValidateDate(It.IsAny<string>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'value')");
        }
    }
}
