using JaVisitei.Brasil.Business.Validation.Expressions;
using JaVisitei.Brasil.Business.Validation.Models;
using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;

namespace JaVisitei.Brasil.Business.Validation.Validators
{
    public class VisitValidator : ModelValidator<VisitResponse>
    {
        public VisitValidator() { }

        public VisitValidator(string[] erros) {
            Errors = erros;
        }

        public void ValidatesVisitCreation(InsertVisitRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            ValidatesVisitKey(request.UserId, request.RegionId, request.RegionTypeId);

            if(string.IsNullOrEmpty(request.Color))
                Errors.Add("Código da cor não informado.");

            else if (!ColorRGBRegex.ValidateColorRGB(request.Color))
                Errors.Add("Código da cor inválido.");

            if (string.IsNullOrEmpty(request.VisitationDate))
                Errors.Add("Informe a data.");

            else if(!DateRegex.ValidateDate(request.VisitationDate))
                Errors.Add("Informe a data no formato correto: DD-MM-AAAA.");
        }

        public void ValidatesVisitDelete(VisitKeyRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            ValidatesVisitKey(request.UserId, request.RegionId, request.RegionTypeId);
        }

        public void ValidatesVisitKey(int userId, string regionId, short regionTypeId)
        {
            if (userId.Equals(0))
                Errors.Add("Informe o código do usuário.");

            if (string.IsNullOrEmpty(regionId))
                Errors.Add("Informe o código da região.");

            else if (!RegionIdRegex.ValidateRegionId(regionId))
                Errors.Add("Informe um código de região válido.");

            if (regionTypeId.Equals(0))
                Errors.Add("Informe um tipo de região válido.");
        }
    }
}
