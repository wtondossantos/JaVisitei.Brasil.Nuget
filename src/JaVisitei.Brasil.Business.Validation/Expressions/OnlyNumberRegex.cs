using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class OnlyNumberRegex
    {
        public static bool ValidateOnlyNumber(string number)
        {
            return BaseValidator.RegexValidade(number, Constant.REGEX_EXPRESSION_ONLY_NUMBER);
        }
    }
}
