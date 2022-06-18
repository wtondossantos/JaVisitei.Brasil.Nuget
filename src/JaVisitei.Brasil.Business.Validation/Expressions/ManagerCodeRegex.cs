using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class ManagerCodeRegex
    {
        public static bool ValidateManagerCode(string code)
        {
            return BaseValidator.RegexValidade(code, Constant.REGEX_EXPRESSION_MANAGER_CODE);
        }
    }
}
