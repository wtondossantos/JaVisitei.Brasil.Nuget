using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Validations
{
    public class VisitValidation
    {
        public List<string> ValidaRegistroVisita(AddVisitRequest model)
        {
            var retorno = new List<string>();

            if (string.IsNullOrEmpty(model.RegionId))
                retorno.Add("Informe uma região.");

            else if (model.RegionTypeId == 0)
                retorno.Add("Informe um tipo de região.");

            return retorno;
        }
    }
}
