using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class VisitRepository : Repository<Visit>, IVisitRepository
    {
        public VisitRepository(DbJaVisiteiBrasilContext context) : base(context) { }

        public async Task<Visit> GetAsync(Visit request)
        {
            return await GetFirstOrDefaultAsync(x => x.UserId.Equals(request.UserId) &&
                            x.RegionId.Equals(request.RegionId) &&
                            x.RegionTypeId.Equals(request.RegionTypeId));
        }

        public Task<bool> AnyAsync(Visit request)
        {
            return AnyAsync(x => x.UserId.Equals(request.UserId) &&
                            x.RegionId.Equals(request.RegionId) &&
                            x.RegionTypeId.Equals(request.RegionTypeId));
        }
    }
}
