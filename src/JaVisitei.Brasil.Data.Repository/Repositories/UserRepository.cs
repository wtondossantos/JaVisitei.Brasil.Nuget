using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
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
                              Actived = user.Actived,
                              Password = user.Password,
                              Username = user.Username,
                              RefreshToken = user.RefreshToken,
                              UserRole = userRoles
                          }).FirstOrDefaultAsync();
        }

        public async Task<User> GetByExternalIdAsync(string id)
        {
            return await (from user in _context.Users
                          join userRoles in _context.UserRoles on user.UserRoleId equals userRoles.Id
                          where user.Id.Equals(id)
                          select new User
                          {
                              Id = user.Id,
                              Name = user.Name,
                              Surname = user.Surname,
                              Email = user.Email,
                              Actived = user.Actived,
                              Password = user.Password,
                              RegistryDate = user.RegistryDate,
                              Username = user.Username,
                              RefreshToken = user.RefreshToken,
                              UserRoleId = userRoles.Id,
                              UserRole = userRoles,
                              SecurityStamp = user.SecurityStamp
                          }).FirstOrDefaultAsync();
        }

        public async Task<User> GetRefreshTokenAsync(string email, string refreshToken)
        {
            return await (from user in _context.Users
                            join userRoles in _context.UserRoles on user.UserRoleId equals userRoles.Id
                            where user.Email.Equals(email) && user.RefreshToken.Equals(refreshToken)
                            select new User
                            {
                                Id = user.Id,
                                Name = user.Name,
                                Surname = user.Surname,
                                Email = user.Email,
                                Actived = user.Actived,
                                Password = user.Password,
                                RegistryDate = user.RegistryDate,
                                Username = user.Username,
                                RefreshToken = user.RefreshToken,
                                UserRoleId = userRoles.Id,
                                UserRole = userRoles,
                                SecurityStamp = user.SecurityStamp
                            }).FirstOrDefaultAsync();
        }
    }
}
