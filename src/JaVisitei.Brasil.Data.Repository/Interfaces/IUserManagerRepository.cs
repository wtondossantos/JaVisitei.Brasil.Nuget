using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IUserManagerRepository : IRepository<UserManager>
    {
        Task<UserManager> CreateAsync(UserManager userManager);
    }
}
