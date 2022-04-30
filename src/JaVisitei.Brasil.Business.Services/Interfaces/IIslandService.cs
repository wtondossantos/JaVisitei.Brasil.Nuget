using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IIslandService : IBaseService<Island>
    {
        Task<IEnumerable<Island>> GetByStateAsync(string id);
        Task<IEnumerable<Island>> GetByMacroregionAsync(string id);
    }
}
