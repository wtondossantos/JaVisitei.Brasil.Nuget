using System.Text.RegularExpressions;

namespace JaVisitei.Brasil.Business.Validation.Base
{
    public class BaseValidator
    {
        public static bool RegexValidade(string value, string expression)
        {
            if (string.IsNullOrEmpty(expression))
                throw new ArgumentNullException(nameof(expression));

            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var regex = new Regex(expression);
            var match = regex.Match(value);

            return match.Success;
        }
    }
}
