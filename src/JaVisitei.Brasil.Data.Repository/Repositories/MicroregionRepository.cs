using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class MicroregionRepository : BaseRepository<Microregion>, IMicroregionRepository
    {
        public MicroregionRepository(DbJaVisiteiBrasilContext context) : base(context) { }

        public async Task<IEnumerable<Microregion>> GetByStateAsync(string id)
        {
            return await GetAsync(x => x.Id.Substring(0, 3).Equals(id.Substring(0, 3)));
        }
    }
}
