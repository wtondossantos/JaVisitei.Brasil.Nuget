using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class RegionIdRegex
    {
        private const string _regexRegionId = @"^(?=.*[a-z_])[a-z_]{1,50}$";

        public static bool ValidateRegionId(string regionId)
        {
            return BaseValidator.RegexValidade(regionId, _regexRegionId);
        }
    }
}
