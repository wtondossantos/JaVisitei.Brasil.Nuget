using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IMunicipalityRepository : IBaseRepository<Municipality>
    {
        Task<IEnumerable<Municipality>> GetByStateAsync(string id);
        Task<IEnumerable<Municipality>> GetByMacroregionAsync(string id);
    }
}
