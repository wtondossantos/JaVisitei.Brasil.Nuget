using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class UsernameRegex
    {
        public static bool ValidateUsername(string username)
        {
            return BaseValidator.RegexValidade(username, Constant.REGEX_EXPRESSION_UNSERNAME);
        }
    }
}
