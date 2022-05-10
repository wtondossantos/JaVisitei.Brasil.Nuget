using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class AlphanumericCodeRegex
    {
        private const string _regexAlphanumericCode = @"^[A-Za-z\d]{10,}$";

        public static bool ValidateAlphanumericCode(string code)
        {
            return BaseValidator.RegexValidade(code, _regexAlphanumericCode);
        }
    }
}
