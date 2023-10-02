using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Profile
{
    public class ResetPasswordResponse
    {
        [JsonPropertyName("redefined")]
        public bool Redefined { get; set; }

        [JsonPropertyName("user_email")]
        public string UserEmail { get; set; }
        
    }
}
