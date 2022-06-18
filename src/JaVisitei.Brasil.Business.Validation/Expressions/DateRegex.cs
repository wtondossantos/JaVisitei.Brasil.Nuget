using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class DateRegex
    {
        public static bool ValidateDate(string date)
        {
            return BaseValidator.RegexValidade(date, Constant.REGEX_EXPRESSION_DATE);
        }
    }
}
