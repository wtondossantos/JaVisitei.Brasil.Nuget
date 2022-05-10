using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class OnlyTextRegex
    {
        private const string _regexRegionId = @"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$";

        public static bool ValidateOnlyText(string regionId)
        {
            return BaseValidator.RegexValidade(regionId, _regexRegionId);
        }
    }
}
