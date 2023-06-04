using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Threading.Tasks;
using JaVisitei.Brasil.Business.ViewModels.Request.UserManager;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IUserManagerService : IService<UserManager>
    {
        Task<UserManager> CreateAsync(InsertEmailConfirmationUserManagerRequest request);
        Task<UserManager> CreateEmailConfirmationAsync(string userId);
        Task<UserManager> CreateAsync(InsertPasswordResetUserManagerRequest request);
        Task<UserManager> CreatePasswordResetAsync(string userId);
        Task<UserManager> GetByManagerCodeAsync(string code);
        Task<bool> ConfirmedChangeAsync(UserManager userManager);
    }
}
