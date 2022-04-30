using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.ViewModels.Response.Profile;
using JaVisitei.Brasil.Business.ViewModels.Response.User;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IProfileService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
