using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class UsernameRegex
    {
        private const string _regexUsername = @"^[A-Za-z\d_]{3,50}$";

        public static bool ValidateUsername(string username)
        {
            return BaseValidator.RegexValidade(username, _regexUsername);
        }
    }
}
