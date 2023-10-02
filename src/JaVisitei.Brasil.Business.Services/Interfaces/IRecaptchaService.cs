using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.Recaptcha;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IRecaptchaService
    {
        Task<RecaptchaValidator> RetrieveAsync(RecaptchaRequest request);
    }
}
