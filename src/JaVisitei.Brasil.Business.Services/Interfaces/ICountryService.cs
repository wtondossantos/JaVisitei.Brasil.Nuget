using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Threading.Tasks;
using JaVisitei.Brasil.Business.ViewModels.Response.Country;
using System.Collections.Generic;
using JaVisitei.Brasil.Business.ViewModels.Response.Base;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface ICountryService : IReadOnlyService<Country>
    {
        Task<List<CountryResponse>> GetByMapTypeIdAsync(short mapTypeId);
        Task<List<CountryResponse>> GetFullByMapTypeIdAsync(short mapTypeId);
        Task<List<CountryResponse>> GetByMapTypeIdAndUserIdAsync(short mapTypeId, string userId);
        Task<List<CountryResponse>> GetFullByMapTypeIdAndUserIdAsync(short mapTypeId, string userId);
        Task<CountryResponse> GetFullByIdAsync(string countryId);
        Task<CountryResponse> GetFullByIdAndUserIdAsync(string countryId, string userId);
        Task<List<BasicResponse>> GetNamesAsync(short mapTypeId);
    }
}
