using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface ICountryService : IReadOnlyService<Country>
    {
        Task<Country> GetFullByIdAsync(string countryId);
    }
}
