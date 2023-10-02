using JaVisitei.Brasil.Business.Validation.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Validation.Test.Expressions
{
    [TestClass]
    public class ColorRGBRegexTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod("Return valid ColorRGB 255,255,255")]
        public void ValidateColorRGB_ExpresionIsValid_SucsessWhite()
        {
            var colorRGB = "255,255,255";

            var result = ColorRGBRegex.ValidateColorRGB(colorRGB);

            Assert.IsTrue(result);
        }

        [TestMethod("Return valid ColorRGB 55,55,55")]
        public void ValidateColorRGB_ExpresionIsValid_SucsessGray()
        {
            var colorRGB = "55,55,55";

            var result = ColorRGBRegex.ValidateColorRGB(colorRGB);

            Assert.IsTrue(result);
        }

        [TestMethod("Return valid ColorRGB 5,5,5")]
        public void ValidateColorRGB_ExpresionIsValid_SucsessBlack1()
        {
            var colorRGB = "5,5,5";

            var result = ColorRGBRegex.ValidateColorRGB(colorRGB);

            Assert.IsTrue(result);
        }

        [TestMethod("Return valid ColorRGB 0,0,0")]
        public void ValidateColorRGB_ExpresionIsValid_SucsessBlack2()
        {
            var colorRGB = "0,0,0";

            var result = ColorRGBRegex.ValidateColorRGB(colorRGB);

            Assert.IsTrue(result);
        }

        [TestMethod("Return invalid ColorRGB with special character")]
        public void ValidateColorRGB_ExpresionIsInvalid_WithSpecialCharacter()
        {
            var colorRGB = "255,255;255";

            var result = ColorRGBRegex.ValidateColorRGB(colorRGB);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ColorRGB with space")]
        public void ValidateColorRGB_ExpresionIsInvalid_WithSpace()
        {
            var colorRGB = "255,255 255";

            var result = ColorRGBRegex.ValidateColorRGB(colorRGB);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ColorRGB with only one at")]
        public void ValidateColorRGB_ExpresionIsInvalid_OnlyOneAt()
        {
            var colorRGB = "0,00";

            var result = ColorRGBRegex.ValidateColorRGB(colorRGB);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ColorRGB with 255,255,256")]
        public void ValidateColorRGB_ExpresionIsInvalid_ValueInvalid1()
        {
            var colorRGB = "255,255,256";

            var result = ColorRGBRegex.ValidateColorRGB(colorRGB);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ColorRGB with 255,255,265")]
        public void ValidateColorRGB_ExpresionIsInvalid_ValueInvalid2()
        {
            var colorRGB = "255,255,265";

            var result = ColorRGBRegex.ValidateColorRGB(colorRGB);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ColorRGB with 255,255,355")]
        public void ValidateColorRGB_ExpresionIsInvalid_ValueInvalid3()
        {
            var colorRGB = "255,255,355";

            var result = ColorRGBRegex.ValidateColorRGB(colorRGB);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ColorRGB format invalid")]
        public void ValidateUsername_ExpresionIsInvalid_FormatInvalid()
        {
            var colorRGB = "255,255,1255";

            var result = ColorRGBRegex.ValidateColorRGB(colorRGB);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ColorRGB format invalid with comma")]
        public void ValidateUsername_ExpresionIsInvalid_FormatInvalidWithComma()
        {
            var colorRGB = "255,255,125,5";

            var result = ColorRGBRegex.ValidateColorRGB(colorRGB);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ColorRGB empty")]
        public void ValidateColorRGB_ExpresionIsInvalid_Empty()
        {
            var colorRGB = string.Empty;

            var result = ColorRGBRegex.ValidateColorRGB(colorRGB);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid ColorRGB exception")]
        public void ValidateColorRGB_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => ColorRGBRegex.ValidateColorRGB(It.IsAny<string>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'rgb')");
        }
    }
}
