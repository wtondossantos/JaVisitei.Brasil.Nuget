using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IIslandRepository : IReadOnlyRepository<Island>
    {
        Task<IEnumerable<Island>> GetByStateAsync(string stateId);
        Task<IEnumerable<Island>> GetByMacroregionAsync(string macroregionId);
    }
}
