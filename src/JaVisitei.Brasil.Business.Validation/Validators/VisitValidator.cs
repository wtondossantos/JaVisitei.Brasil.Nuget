using JaVisitei.Brasil.Business.Validation.Expressions;
using JaVisitei.Brasil.Business.Validation.Models;
using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;

namespace JaVisitei.Brasil.Business.Validation.Validators
{
    public class VisitValidator : ModelValidator<VisitResponse>
    {
        public VisitValidator() { }

        public void ValidatesVisitCreation(AddVisitRequest request)
        {
            if (String.IsNullOrEmpty(request.RegionId))
                Errors.Add("Informe o código da região.");

            else if(!RegionIdRegex.ValidateRegionId(request.RegionId.ToLower()))
                Errors.Add("Informe um código de região válido.");

            if (request.RegionTypeId.Equals(0))
                Errors.Add("Informe um tipo de região válido.");

            if(!ColorRGBRegex.ValidateColorRGB(request.Color))
                Errors.Add("Código da cor inválido.");
        }
    }
}
