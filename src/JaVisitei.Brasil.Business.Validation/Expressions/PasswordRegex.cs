using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class PasswordRegex
    {
        public static bool ValidatePassword(string password)
        {
            return BaseValidator.RegexValidade(password, Constant.REGEX_EXPRESSION_PASSWORD);
        }
    }
}
