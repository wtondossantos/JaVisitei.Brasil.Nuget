using JaVisitei.Brasil.Helper.Formatting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Cryptography;
using System.Text;

namespace JaVisitei.Brasil.Helper.Test.Formatting
{
    [TestClass]
    public class FormatTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        #region Byte array to string

        [TestMethod("Return valid Byte array to string")]
        public void ByteArrayToString_FormatIsValid_Sucsess()
        {
            var phrase = "teste";
            var sha256hasher = SHA256.Create();
            var hashedDataBytes = sha256hasher.ComputeHash(new UTF8Encoding().GetBytes(phrase));

            var result = Format.ByteArrayToString(hashedDataBytes);

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }

        [TestMethod("Return invalid Byte array to string empty")]
        public void ByteArrayToString_FormatIsInvalid_Empty()
        {
            var hashedDataBytes = new byte[] { };

            var result = Format.ByteArrayToString(hashedDataBytes);

            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod("Return invalid Byte array to string exception")]
        public void ByteArrayToString_FormatIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => Format.ByteArrayToString(It.IsAny<byte[]>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'inputArray')");
        }

        #endregion

        #region Email trace ability code string

        [TestMethod("Return valid Email trace ability code string")]
        public void EmailTraceabilityCodeString_FormatIsValid_Sucsess()
        {
            var primary = 11;
            int secundary = 12;
            var response = "000000001100012";

            var result = Format.EmailTraceabilityCodeString(primary, secundary);

            Assert.AreEqual(response, result);
            Assert.IsTrue(result.Length.Equals(15));
        }

        [TestMethod("Return invalid Email trace ability code string empty")]
        public void EmailTraceabilityCodeString_FormatIsInvalid_Empty()
        {
            var response = "000000000000000";
            var result = Format.EmailTraceabilityCodeString(It.IsAny<int>(), It.IsAny<int>());

            Assert.AreEqual(response, result);
            Assert.IsTrue(result.Length.Equals(15));
        }

        #endregion

        #region Convert RGB To Hexdecimal

        [TestMethod("Return valid RGB convertion")]
        public void ConvertRGBToHex_FormatIsValid_Sucsess()
        {
            var rgb = "255,0,255";
            var response = "#FF00FF";

            var result = Format.ConvertRGBToHex(rgb);

            Assert.AreEqual(response, result);
            Assert.IsTrue(result.Length.Equals(7));
        }

        [TestMethod("Return invalid RGB convertion string nullable")]
        public void ConvertRGBToHex_FormatIsInvalid_Nullable()
        {
            var result = Format.ConvertRGBToHex(It.IsAny<string>());

            Assert.IsNull(result);
        }

        [TestMethod("Return invalid RGB convertion string empty")]
        public void ConvertRGBToHex_FormatIsInvalid_Empty()
        {
            var result = Format.ConvertRGBToHex(string.Empty);

            Assert.AreEqual(string.Empty,result);
        }

        #endregion


    }
}
