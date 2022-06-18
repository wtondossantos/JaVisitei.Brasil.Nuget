using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IMunicipalityRepository : IReadOnlyRepository<Municipality>
    {
        Task<IEnumerable<Municipality>> GetByStateAsync(string stateId);
        Task<IEnumerable<Municipality>> GetByMacroregionAsync(string macroregionId);
    }
}
