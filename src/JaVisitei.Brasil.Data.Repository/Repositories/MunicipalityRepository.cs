using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class MunicipalityRepository : BaseRepository<Municipality>, IMunicipalityRepository
    {
        private IMicroregionRepository _microregionRepo;

        public MunicipalityRepository(DbJaVisiteiBrasilContext context) : base(context)
        {
            _microregionRepo = new MicroregionRepository(context);
        }

        public async Task<IEnumerable<Municipality>> GetByStateAsync(string id)
        {
            return await GetAsync(x => x.Id.Substring(0, 3).Equals(id.Substring(0, 3)));
        }

        public async Task<IEnumerable<Municipality>> GetByMacroregionAsync(string id)
        {
            var microregions = await _microregionRepo.GetAsync(x => x.MacroregionId.Equals(id));
            var municipalities = new List<Municipality>();

            foreach (var microregion in microregions)
            {
                var municipality = await GetAsync(x => x.MicroregionId.Equals(microregion.Id));
                municipalities.AddRange(municipality);
            }
            return municipalities;
        }
    }
}
