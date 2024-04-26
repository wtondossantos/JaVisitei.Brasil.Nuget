using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> LoginAsync(string input, string password);
        Task<User> GetRefreshTokenAsync(string id, string refreshToen);
    }
}
