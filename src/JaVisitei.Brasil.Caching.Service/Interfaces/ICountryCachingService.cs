using JaVisitei.Brasil.Business.CacheModels;
using JaVisitei.Brasil.Business.ViewModels.Response.Base;
using JaVisitei.Brasil.Business.ViewModels.Response.Country;

namespace JaVisitei.Brasil.Caching.Service.Interfaces
{
    public interface ICountryCachingService
    {
        Task<CountryCaching> GetAsync(string countryId);
        Task<CountriesCaching> GetByMapTypeIdAsync(short mapTypeId);
        Task<CountriesCaching> GetFullByMapTypeIdAsync(short mapTypeId);
        Task<List<BasicResponse>> GetNamesAsync(short mapTypeId, List<CountryResponse> countries);
    }
}
