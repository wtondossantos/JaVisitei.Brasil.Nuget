using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class MunicipalityRepository : ReadOnlyRepository<Municipality>, IMunicipalityRepository
    {
        public MunicipalityRepository(DbJaVisiteiBrasilContext context) : base(context) { }

        public async Task<IEnumerable<Municipality>> GetByStateAsync(string stateId)
        {
            return await GetAsync(x => x.Id.Substring(0, 3).Equals(stateId.Substring(0, 3)));
        }

        public async Task<IEnumerable<Municipality>> GetByMacroregionAsync(string macroregionId)
        {
            return await (from municipality in _context.Municipalities
                          join microregion in _context.Microregions on municipality.MicroregionId equals microregion.Id
                          where microregion.MacroregionId.Equals(macroregionId)
                          select municipality).ToListAsync();
        }
    }
}
