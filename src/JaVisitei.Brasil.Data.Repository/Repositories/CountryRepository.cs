using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class CountryRepository : ReadOnlyRepository<Country>, ICountryRepository
    {
        public CountryRepository(DbJaVisiteiBrasilContext context) : base(context) { }
    }
}
