using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Threading.Tasks;
using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.Validation.Validators;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<UserValidator> AddAsync(AddUserRequest request);
        Task<UserValidator> AddAsync(AddFullUserRequest request);
        Task<UserValidator> EditAsync(EditUserRequest request);
        Task<UserValidator> EditAsync(EditFullUserRequest request);
    }
}
