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
    public class IslandRepository : ReadOnlyRepository<Island>, IIslandRepository
    {
        public IslandRepository(DbJaVisiteiBrasilContext context) : base(context) { }

        public async Task<IEnumerable<Island>> GetByStateAsync(string stateId)
        {
            return await GetAsync(x => x.Id.Substring(0, 3).Equals(stateId.Substring(0, 3)));
        }

        public async Task<IEnumerable<Island>> GetByMacroregionAsync(string macroregionId)
        {
            return await (from island in _context.Islands
                          join archipelago in _context.Archipelagos on island.ArchipelagoId equals archipelago.Id
                          where archipelago.MacroregionId.Equals(macroregionId)
                          select island).ToListAsync();
        }
    }
}
