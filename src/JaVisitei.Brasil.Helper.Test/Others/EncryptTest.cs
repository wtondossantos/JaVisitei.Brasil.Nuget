using JaVisitei.Brasil.Helper.Others;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Helper.Test.Others
{
    [TestClass]
    public class EncryptTest
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod("Return valid Sha256 encrypt")]
        public void Sha256encrypt_FormatIsValid_Sucsess()
        {
            var phrase = "teste";

            var result = Encrypt.Sha256encrypt(phrase);

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }

        [TestMethod("Return invalid Sha256 encrypt exception")]
        public void Sha256encrypt_FormatIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => Encrypt.Sha256encrypt(It.IsAny<string>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'phrase')");
        }
    }
}
