using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class EmailRegex
    {
        public static bool ValidateEmail(string email)
        {
            if (email is null)
                throw new ArgumentNullException(nameof(email));

            return BaseValidator.RegexValidade(email.ToLower(), Constant.REGEX_EXPRESSION_EMAIL);
        }
    }
}
