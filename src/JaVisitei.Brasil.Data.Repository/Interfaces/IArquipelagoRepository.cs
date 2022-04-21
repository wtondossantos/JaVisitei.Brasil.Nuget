using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IArquipelagoRepository : IBaseRepository<Arquipelago>
    {
        Task<IEnumerable<Arquipelago>> PesquisarPorEstadoAsync(string id);
    }
}
