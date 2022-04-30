using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Threading.Tasks;
using JaVisitei.Brasil.Business.ViewModels.Response.Profile;
using JaVisitei.Brasil.Business.ViewModels.Response.User;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using JaVisitei.Brasil.Business.ViewModels.Request.User;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<AddUserResponse> AddAsync(AddUserRequest request);
        Task<EditUserResponse> EditAsync(EditUserRequest request);
    }
}
