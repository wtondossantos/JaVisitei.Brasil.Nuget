using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.Recaptcha;
using JaVisitei.Brasil.Business.ViewModels.Response.Recaptcha;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class RecaptchaService : IRecaptchaService
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly RecaptchaValidator _recaptchaValidator;

        public RecaptchaService(RecaptchaValidator recaptchaValidator)
        {
            _recaptchaValidator = recaptchaValidator;
        }

        public async Task<RecaptchaValidator> RetrieveAsync(RecaptchaRequest request)
        {
            try
            {
                _recaptchaValidator.ValidatesKey(request);

                if (!_recaptchaValidator.IsValid)
                    return _recaptchaValidator;

                var values = new Dictionary<string, string> {
                    { "secret", Environment.GetEnvironmentVariable("RECAPTCHA_SECRET_KEY")},
                    { "response", request.Response}
                };
                
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(Environment.GetEnvironmentVariable("RECAPTCHA_GOOGLE_URL"), content);
                var responseString = await response.Content.ReadAsStringAsync();
                var captchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(responseString);

                if(captchaResponse.Success)
                {
                    _recaptchaValidator.Data = captchaResponse;
                    _recaptchaValidator.Message = "Sucesso";

                    return _recaptchaValidator;
                }

                _recaptchaValidator.Errors = captchaResponse.ErrorCodes;
            }
            catch
            {
                _recaptchaValidator.Errors.Add("Erro ao tentar enviar mensagem de confirmação.");
                throw;
            }

            return _recaptchaValidator;

        }
    }
}
