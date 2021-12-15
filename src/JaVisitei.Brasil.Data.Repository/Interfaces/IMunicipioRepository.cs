using JaVisitei.Brasil.Model.Models;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IMunicipioRepository : IBaseRepository<Municipio>
    {
        IEnumerable<Municipio> PesquisarPorEstado(string id);
    }
}
