using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IMunicipalityService : IBaseService<Municipality>
    {
        Task<IEnumerable<Municipality>> GetByStateAsync(string id);
        Task<IEnumerable<Municipality>> GetByMacroregionAsync(string id);
    }
}
