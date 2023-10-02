using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.Validation.Base;

namespace JaVisitei.Brasil.Business.Validation.Expressions
{
    public static class RegionIdRegex
    {
        public static bool ValidateRegionId(string regionId)
        {
            if (regionId is null)
                throw new ArgumentNullException(nameof(regionId));

            return BaseValidator.RegexValidade(regionId.ToLower(), Constant.REGEX_EXPRESSION_REGION_ID);
        }
    }
}
