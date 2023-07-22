using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;

namespace JaVisitei.Brasil.Caching.Service.Interfaces
{
    public interface IMunicipalityCachingService
    {
        Task<List<MunicipalityBasicResponse>> GetByCountryIdAsync(string countryId);
    }
}
