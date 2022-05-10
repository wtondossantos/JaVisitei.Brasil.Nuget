using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class EmailRegex
    {
        private const string _regexEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        public static bool ValidateEmail(string email)
        {
            return BaseValidator.RegexValidade(email, _regexEmail);
        }
    }
}
