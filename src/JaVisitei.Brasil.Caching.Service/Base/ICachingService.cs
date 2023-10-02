
namespace JaVisitei.Brasil.Caching.Service.Base
{
    public interface ICachingService
    {
        Task SetAsync(string key, string value, double expiration);
        Task<string> GetAsync(string key);
    }
}
