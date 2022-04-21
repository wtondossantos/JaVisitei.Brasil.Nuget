using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IIlhaRepository : IBaseRepository<Ilha>
    {
        Task<IEnumerable<Ilha>> PesquisarPorEstadoAsync(string id);
        Task<IEnumerable<Ilha>> PesquisarPorMesorregiaoAsync(string id);
    }
}
