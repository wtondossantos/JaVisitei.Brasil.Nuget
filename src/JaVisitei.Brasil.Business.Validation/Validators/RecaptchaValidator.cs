using JaVisitei.Brasil.Business.Validation.Models;
using JaVisitei.Brasil.Business.ViewModels.Request.Recaptcha;
using JaVisitei.Brasil.Business.ViewModels.Response.Recaptcha;

namespace JaVisitei.Brasil.Business.Validation.Validators
{
    public class RecaptchaValidator : ModelValidator<RecaptchaResponse>
    { 
        public RecaptchaValidator() { }

        public void ValidatesKey(RecaptchaRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrEmpty(request.Key))
                Errors.Add("Informe a chave correta.");

            else if (!request.Key.Equals(Environment.GetEnvironmentVariable("RECAPTCHA_CLIENT_KEY")))
                Errors.Add("Informe a chave correta.");

            if (string.IsNullOrEmpty(request.Response))
                Errors.Add("Informe o Response correto.");
            
        }

    }
}
