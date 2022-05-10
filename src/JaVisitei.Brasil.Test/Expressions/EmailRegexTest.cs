using JaVisitei.Brasil.Business.Validation.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JaVisitei.Brasil.Empressions.Test
{
    [TestClass]
    public class EmailRegexTest
    {
        public EmailRegexTest()
        {
        }

        [TestMethod]
        public void Autentication_IsValid_EmailRegex()
        {
            //Arrange
            var email = "wellington@wton.com.br";

            //Act
            var result = EmailRegex.ValidateEmail(email);

            //Assert
            Assert.IsTrue(result);
        }
    }
}