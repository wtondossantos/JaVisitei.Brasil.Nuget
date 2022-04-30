using JaVisitei.Brasil.Helper.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JaVisitei.Brasil.Helper.Test
{
    [TestClass]
    public class ValidarTest
    {
        private Validate _validate;

        public ValidarTest()
        {
            _validate = new Validate();
        }

        [TestMethod]
        public void Autenticacao_IsValid_DadosUEntrada()
        {
            //Arrange
            var email = "wellington@wton.com.br";

            //Act
            var result = _validate.ValidateEmail(email);

            //Assert
            Assert.IsTrue(result);
        }
    }
}