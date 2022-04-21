using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IMunicipioRepository : IBaseRepository<Municipio>
    {
        Task<IEnumerable<Municipio>> PesquisarPorEstadoAsync(string id);
        Task<IEnumerable<Municipio>> PesquisarPorMesorregiaoAsync(string id);
    }
}
