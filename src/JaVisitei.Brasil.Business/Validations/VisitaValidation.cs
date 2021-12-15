using JaVisitei.Brasil.Business.ViewModels.Request;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Validations
{
    public class VisitaValidation
    {
        public List<string> ValidaRegistroVisita(VisitaAdicionarRequest model)
        {
            var retorno = new List<string>();

            if (string.IsNullOrEmpty(model.IdRegiao))
                retorno.Add("Informe uma região.");

            else if (model.IdTipoRegiao == 0)
                retorno.Add("Informe um tipo de região.");

            return retorno;
        }
    }
}
