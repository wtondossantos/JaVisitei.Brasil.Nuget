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
            ValidatesVisitDate(request.VisitationDate);
            ValidatesVisiColor(request.Color);
            ValidatesVisitNote(request.Note);
        }

        public void ValidatesVisitEdition(UpdateVisitRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            ValidatesVisitKey(request.UserId, request.RegionId, request.RegionTypeId);
            ValidatesVisitDate(request.VisitationDate);
            ValidatesVisiColor(request.Color);
            ValidatesVisitNote(request.Note);

        }

        public void ValidatesVisitDelete(VisitKeyRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            ValidatesVisitKey(request.UserId, request.RegionId, request.RegionTypeId);
        }

        public void ValidatesVisitNote(string note)
        {
            if (!string.IsNullOrEmpty(note) && note.Length > 255)
                Errors.Add("Informe uma anotação menor que 255 caracteres.");
        }

        public void ValidatesVisitDate(string date)
        {
            if (string.IsNullOrEmpty(date))
                Errors.Add("Informe a data.");

            else if (!DateRegex.ValidateDate(date))
                Errors.Add("Informe a data no formato correto: yyyy-MM-dd.");
        }

        public void ValidatesVisiColor(string color)
        {
            if (string.IsNullOrEmpty(color))
                Errors.Add("Código da cor não informado.");

            else if (!ColorRGBRegex.ValidateColorRGB(color))
                Errors.Add("Código da cor inválido.");
        }

        public void ValidatesVisitKey(string userId, string regionId, short regionTypeId)
        {
            if (string.IsNullOrEmpty(userId))
                Errors.Add("Informe o código do usuário.");

            else if(userId.Length != 36)
                Errors.Add("Informe o código do usuário válido.");

            if (regionTypeId.Equals(0))
                Errors.Add("Informe um tipo de região válido.");

            if (string.IsNullOrEmpty(regionId))
                Errors.Add("Informe o código da região.");

            else if (!RegionIdRegex.ValidateRegionId(regionId) && (regionTypeId.Equals(6) || regionTypeId.Equals(7)))
                Errors.Add("Informe um código de região válido.");
        }
    }
}
