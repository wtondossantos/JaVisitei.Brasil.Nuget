using JaVisitei.Brasil.Business.Validation.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Validation.Test.Expressions
{
    [TestClass]
    public class RegionIdRegexTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod("Return valid RegionId")]
        public void ValidateRegionId_ExpresionIsValid_Sucsess()
        {
            var regionId = "abc_def";

            var result = RegionIdRegex.ValidateRegionId(regionId);

            Assert.IsTrue(result);
        }

        [TestMethod("Return Valid RegionId with uppercase")]
        public void ValidateRegionId_ExpresionIsValid_WithUppercase()
        {
            var regionId = "ABC_DEF";

            var result = RegionIdRegex.ValidateRegionId(regionId);

            Assert.IsTrue(result);
        }

        [TestMethod("Return invalid RegionId with special character")]
        public void ValidateRegionId_ExpresionIsInvalid_WithSpecialCharacter()
        {
            var regionId = "abc@def";

            var result = RegionIdRegex.ValidateRegionId(regionId);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid RegionId with number")]
        public void ValidateRegionId_ExpresionIsInvalid_WithNumber()
        {
            var regionId = "abc1def";

            var result = RegionIdRegex.ValidateRegionId(regionId);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid RegionId with space")]
        public void ValidateRegionId_ExpresionIsInvalid_WithSpace()
        {
            var regionId = "abc def";

            var result = RegionIdRegex.ValidateRegionId(regionId);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid RegionId maximum length")]
        public void ValidateRegionId_ExpresionIsInvalid_MaximumLength()
        {
            var regionId = "pneumonoultramicroscopicsilicovolcanoconiosis_maximum";

            var result = RegionIdRegex.ValidateRegionId(regionId);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid RegionId empty")]
        public void ValidateRegionId_ExpresionIsInvalid_Empty()
        {
            var regionId = string.Empty;

            var result = RegionIdRegex.ValidateRegionId(regionId);

            Assert.IsFalse(result);
        }

        [TestMethod("Return invalid RegionId exception")]
        public void ValidateRegionId_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => RegionIdRegex.ValidateRegionId(It.IsAny<string>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'regionId')");
        }
    }
}
