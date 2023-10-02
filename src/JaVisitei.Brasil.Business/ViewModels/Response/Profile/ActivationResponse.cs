using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Profile
{
    public class ActivationResponse
    {
        [JsonPropertyName("active")]
        public bool Actived { get; set; }

        [JsonPropertyName("user_email")]
        public string UserEmail { get; set; }
    }
}
