using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IMunicipioRepository : IBaseRepository<Municipio>
    {
        IEnumerable<Municipio> PesquisarPorEstado(string id);
        IEnumerable<Municipio> PesquisarPorMesorregiao(string id);
    }
}
