using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Recaptcha
{
    public class RecaptchaResponse
    {
        [JsonProperty("success")]
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        [JsonPropertyName("error-codes")]
        public IList<string> ErrorCodes { get; set; }
    }
}
