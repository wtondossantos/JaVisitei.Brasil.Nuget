using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IMunicipioService : IBaseService<Municipio>
    {
        IEnumerable<Municipio> PesquisarPorEstado(string id);
        IEnumerable<Municipio> PesquisarPorMesorregiao(string id);
    }
}
