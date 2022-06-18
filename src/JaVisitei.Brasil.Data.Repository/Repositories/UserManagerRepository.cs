using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class UserManagerRepository : Repository<UserManager>, IUserManagerRepository
    {
        public UserManagerRepository(DbJaVisiteiBrasilContext context) : base(context) { }

        public async Task<UserManager> CreateAsync(UserManager userManager)
        {
            if (await InsertAsync(userManager))
            {
                return await GetFirstOrDefaultAsync(x =>
                                    x.ManagerCode.Equals(userManager.ManagerCode) &&
                                    x.EmailId.Equals(userManager.EmailId) &&
                                    x.UserId.Equals(userManager.UserId),
                                orderBy: o => o.OrderByDescending(r => r.ExpirationDate));
            }

            return null;
        }
    }
}
