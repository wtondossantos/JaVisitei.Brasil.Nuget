using System;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response
{
    public class LoginResponse
    {
        [JsonPropertyName("expiracao")]
        public DateTime Expira { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("validacao")]
        public ValidacaoResponse Validacao { get; set; }
    }
}
