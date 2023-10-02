using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IMicroregionService : IReadOnlyService<Microregion>
    {
        Task<IEnumerable<M>> GetByStateAsync<M>(string stateId);
    }
}
