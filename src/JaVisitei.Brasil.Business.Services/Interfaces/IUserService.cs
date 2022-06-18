using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Threading.Tasks;
using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.Validation.Validators;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IUserService : IService<User>
    {
        Task<UserValidator> InsertAsync(InsertUserRequest request);
        Task<UserValidator> InsertAsync(InsertFullUserRequest request);
        Task<UserValidator> UpdateAsync(UpdateUserRequest request);
        Task<UserValidator> UpdateAsync(UpdateFullUserRequest request);
        Task<M> LoginAsync<M>(string email, string password);
    }
}
