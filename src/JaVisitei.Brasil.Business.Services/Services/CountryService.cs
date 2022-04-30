using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;

namespace JaVisitei.Brasil.Business.Service
{
    public class CountryService : BaseService<Country>, ICountryService
    {
        public CountryService(ICountryRepository countryRepository) : base(countryRepository) { }
    }
}
