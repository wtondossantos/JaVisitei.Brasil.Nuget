using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class OnlyTextRegex
    {
        public static bool ValidateOnlyText(string regionId)
        {
            return BaseValidator.RegexValidade(regionId, Constant.REGEX_EXPRESSION_ONLY_TEXT);
        }
    }
}
