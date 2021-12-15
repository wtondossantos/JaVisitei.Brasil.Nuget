using System.Text.RegularExpressions;

namespace JaVisitei.Brasil.Helper
{
    public class Validar
    {
        private const string _regexEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        public bool ValidarEmail(string email)
        {
            Regex regex = new Regex(_regexEmail);
            Match match = regex.Match(email);

            return match.Success;
        }
    }
}
