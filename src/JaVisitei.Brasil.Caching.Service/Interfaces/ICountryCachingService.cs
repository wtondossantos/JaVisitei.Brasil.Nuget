using JaVisitei.Brasil.Business.CacheModels;

namespace JaVisitei.Brasil.Caching.Service.Interfaces
{
    public interface ICountryCachingService
    {
        Task<CountryCaching> GetAsync(string countryId);
    }
}
