using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class ColorRGBRegex
    {
        private const string _regexRGB = @"^([0-2]|[1-5]|[0-5]{1,3})(,)([0-2]|[1-5]|[0-5]{1,3})(,)([0-2]|[1-5]|[0-5]{1,3})$";

        public static bool ValidateColorRGB(string rgb)
        {
            return BaseValidator.RegexValidade(rgb, _regexRGB);
        }
    }
}
