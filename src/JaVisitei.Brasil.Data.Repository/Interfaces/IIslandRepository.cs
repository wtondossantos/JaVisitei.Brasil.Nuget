using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IIslandRepository : IBaseRepository<Island>
    {
        Task<IEnumerable<Island>> GetByStateAsync(string id);
        Task<IEnumerable<Island>> GetByMacroregionAsync(string id);
    }
}
