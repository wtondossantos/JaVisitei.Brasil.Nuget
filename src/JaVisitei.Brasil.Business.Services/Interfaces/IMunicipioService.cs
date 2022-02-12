using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service
{
    public interface IMunicipioService : IBaseService<Municipio>
    {
        IEnumerable<Municipio> PesquisarPorEstado(string id);
    }
}
