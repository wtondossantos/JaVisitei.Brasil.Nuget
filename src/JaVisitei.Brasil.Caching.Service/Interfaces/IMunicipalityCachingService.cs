using JaVisitei.Brasil.Business.ViewModels.Response.Base;
using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;

namespace JaVisitei.Brasil.Caching.Service.Interfaces
{
    public interface IMunicipalityCachingService
    {
        Task<List<BasicResponse>> GetNamesByCountryAsync(string countryId, List<MunicipalityResponse> municipalities);
    }
}
