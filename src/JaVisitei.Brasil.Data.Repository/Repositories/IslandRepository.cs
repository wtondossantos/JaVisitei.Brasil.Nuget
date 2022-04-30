using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class IslandRepository : BaseRepository<Island>, IIslandRepository
    {
        private IArchipelagoRepository _archipelagoRepo;

        public IslandRepository(DbJaVisiteiBrasilContext context) : base(context)
        {
            _archipelagoRepo = new ArchipelagoRepository(context);
        }

        public async Task<IEnumerable<Island>> GetByStateAsync(string id)
        {
            return await GetAsync(x => x.Id.Substring(0, 3) == id.Substring(0, 3));
        }

        public async Task<IEnumerable<Island>> GetByMacroregionAsync(string id)
        {
            var macroregions = await _archipelagoRepo.GetAsync(x => x.MacroregionId == id);
            var islands = new List<Island>();

            foreach (var macroregion in macroregions)
            {
                var island = await GetAsync(x => x.ArchipelagoId == macroregion.Id);
                islands.AddRange(island);
            }
            return islands;
        }
    }
}
