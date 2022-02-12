using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JaVisitei.Brasil.Helper.Test
{
    [TestClass]
    public class ValidarTest
    {
        private Validar _Validar;

        public ValidarTest()
        {
            _Validar = new Validar();
        }

        [TestMethod]
        public void Autenticacao_IsValid_DadosUEntrada()
        {
            //Arrange
            var email = "wellington@wton.com.br";

            //Act
            var result = _Validar.ValidarEmail(email);

            //Assert
            Assert.IsTrue(result);
        }
    }
}