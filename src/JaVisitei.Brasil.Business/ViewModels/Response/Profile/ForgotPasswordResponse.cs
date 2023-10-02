using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Profile
{
    public class ForgotPasswordResponse
    {
        [JsonPropertyName("requested")]
        public bool Requested { get; set; }

        [JsonPropertyName("user_email")]
        public string UserEmail { get; set; }
    }
}
