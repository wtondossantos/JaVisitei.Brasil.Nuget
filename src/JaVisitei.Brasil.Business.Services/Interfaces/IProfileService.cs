using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using JaVisitei.Brasil.Business.ViewModels.Response.Profile;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileValidator<LoginResponse>> LoginAsync(LoginRequest request);
        Task<ProfileValidator<ActivationResponse>> ActiveAccountAsync(string activationCode);
        Task<ProfileValidator<ForgotPasswordResponse>> ForgotPasswordAsync(string email);
        Task<ProfileValidator<ResetPasswordResponse>> ResetPasswordAsync(ResetPasswordRequest request);
        
    }
}
