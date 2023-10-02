using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.Recaptcha;
using JaVisitei.Brasil.Business.ViewModels.Response.Recaptcha;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service.Test.Mocks
{
    public static class RecaptchaMock
    {
        public static RecaptchaRequest ReturnKeyRequestMock()
        {
            return new RecaptchaRequest
            {
                Key = "SDFAs534564efFDASFASDGgSDF346546sFSDf",
                Response = "SDFAs534564efFDASFASDGgSDF346546sFSDfSDFAs534564efFDASFASDGgSDF346546sFSDfSDFAs534564efFDASFASDGgSDF346546sFSDf"
            };
        }

        public static RecaptchaValidator ReturnRecaptchaResponseMock()
        {
            return new RecaptchaValidator
            {
                Data = new RecaptchaResponse
                {
                    Success = true
                },
                Message = "Sucess"
            };
        }
    }
}
