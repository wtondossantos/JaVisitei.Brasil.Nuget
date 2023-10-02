using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.Validation.Expressions;
using JaVisitei.Brasil.Helper.Others;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Text.RegularExpressions;

namespace JaVisitei.Brasil.Helper.Test.Others
{
    [TestClass]
    public class UtilityTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        #region Random hex string

        [TestMethod("Return valid Random hex string X8")]
        public void RandomHexString_FormatIsValid_SucsessX8()
        {
            var index = "X8";
            var alphacumeric = @"^[A-Za-z\d]{8}$";

            var result = Utility.RandomHexString(index);

            Assert.IsTrue(result.Length.Equals(8));
            Assert.IsTrue(new Regex(alphacumeric).Match(result).Success);
        }

        [TestMethod("Return invalid Random hex string empty")]
        public void RandomHexString_FormatIsInvalid_Empty()
        {
            var index = string.Empty;

            var result = Utility.RandomHexString(index);

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }

        [TestMethod("Return invalid Random hex string exception")]
        public void RandomHexString_FormatIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => Utility.RandomHexString(It.IsAny<string>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'index')");
        }

        #endregion

        #region Random color RBG string

        [TestMethod("Return valid Random color RBG string")]
        public void RandomColorRBGString_FormatIsValid_Sucsess()
        {
            var result = Utility.RandomColorRBGString();

            Assert.IsTrue(ColorRGBRegex.ValidateColorRGB(result));
        }

        #endregion

        #region Random alphanumeric string

        [TestMethod("Return valid Random alphanumeric string length 10")]
        public void RandomAlphanumericString_FormatIsValid_Sucsess()
        {
            var length = 10;

            var result = Utility.RandomAlphanumericString(length);

            Assert.IsTrue(result.Length.Equals(length));
        }

        [TestMethod("Return valid Random alphanumeric string length 100")]
        public void RandomAlphanumericString_FormatIsValid_Sucsess2()
        {
            var length = 100;

            var result = Utility.RandomAlphanumericString(length);

            Assert.IsTrue(result.Length.Equals(length));
        }

        [TestMethod("Return valid Random alphanumeric string length 0")]
        public void RandomAlphanumericString_FormatIsValid_SucsessEmpty()
        {
            var length = 0;

            var result = Utility.RandomAlphanumericString(It.IsAny<int>());

            Assert.IsTrue(result.Length.Equals(length));
        }

        #endregion
    }
}
