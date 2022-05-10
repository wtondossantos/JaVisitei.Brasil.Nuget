using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbJaVisiteiBrasilContext context) : base(context) { }

        public async Task<User> LoginAsync(string email, string password)
        {
            return await (from user in _context.Users
                          join userRoles in _context.UserRoles on user.UserRoleId equals userRoles.Id
                          where user.Password.Equals(password) && user.Email.Equals(email)
                          select new User
                          {
                              Id = user.Id,
                              Email = user.Email,
                              Password = user.Password,
                              Username = user.Username,
                              UserRole = userRoles
                          }).FirstOrDefaultAsync();
        }
    }
}
