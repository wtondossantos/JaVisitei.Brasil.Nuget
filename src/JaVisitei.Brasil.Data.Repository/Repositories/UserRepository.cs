using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbJaVisiteiBrasilContext context) : base(context) { }

        public async Task<User> LoginAsync(User user)
        {
            var result = await GetAsync(x => x.Password == user.Password && x.Email == user.Email);
            return result.FirstOrDefault();    
        }
    }
}
