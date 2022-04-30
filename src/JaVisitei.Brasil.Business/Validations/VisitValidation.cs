using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Validations
{
    public class VisitValidation
    {
        public List<string> ValidatesVisitCreation(AddVisitRequest request)
        {
            var response = new List<string>();

            if (string.IsNullOrEmpty(request.RegionId))
                response.Add("Informe uma região.");

            else if (request.RegionTypeId == 0)
                response.Add("Informe um tipo de região.");

            return response;
        }
    }
}
