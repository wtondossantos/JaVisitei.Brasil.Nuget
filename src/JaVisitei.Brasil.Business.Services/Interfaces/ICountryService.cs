using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Threading.Tasks;
using JaVisitei.Brasil.Business.ViewModels.Response.Country;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface ICountryService : IReadOnlyService<Country>
    {
        Task<CountryResponse> GetFullByIdAsync(string countryId);
    }
}
