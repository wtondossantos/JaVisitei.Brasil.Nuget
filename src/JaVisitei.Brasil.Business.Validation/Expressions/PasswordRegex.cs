using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class PasswordRegex
    {
        private const string _regexPassword = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d\W]{8,}$";

        public static bool ValidatePassword(string password)
        {
            return BaseValidator.RegexValidade(password, _regexPassword);
        }
    }
}
