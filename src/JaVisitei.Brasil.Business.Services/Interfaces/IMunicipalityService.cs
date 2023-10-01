using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using JaVisitei.Brasil.Business.ViewModels.Response.Base;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IMunicipalityService : IReadOnlyService<Municipality>
    {
        Task<List<BasicResponse>> GetNamesByCountryAsync(string countryId);
        Task<IEnumerable<M>> GetByStateAsync<M>(string stateId);
        Task<IEnumerable<M>> GetByMacroregionAsync<M>(string macroregionId);
    }
}
