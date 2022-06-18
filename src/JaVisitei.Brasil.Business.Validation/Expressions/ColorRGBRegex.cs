using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class ColorRGBRegex
    {
        public static bool ValidateColorRGB(string rgb)
        {
            if (rgb is null)
                throw new ArgumentNullException(nameof(rgb));

            var rgbSplit = rgb.Split(',');
            
            if (rgbSplit.Length < 3)
                return false;

            if (!BaseValidator.RegexValidade(rgb, Constant.REGEX_EXPRESSION_RGB))
                return false;
            
            if(Array.Exists(rgbSplit, x => Convert.ToInt32(x) > 255))
                return false;

            return true;
        }
    }
}
