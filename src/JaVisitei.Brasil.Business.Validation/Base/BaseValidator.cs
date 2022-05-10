using System.Text.RegularExpressions;

namespace JaVisitei.Brasil.Business.Validation.Base
{
    public class BaseValidator
    {
        public static bool RegexValidade(string value, string expression)
        {
            Regex regex = new Regex(expression);
            Match match = regex.Match(value);

            return match.Success;
        }
    }
}
