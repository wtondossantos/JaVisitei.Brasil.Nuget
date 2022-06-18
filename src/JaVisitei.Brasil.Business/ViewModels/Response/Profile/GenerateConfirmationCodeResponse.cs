using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Profile
{
    public class GenerateConfirmationCodeResponse
    {
        [JsonPropertyName("generated")]
        public bool Generated { get; set; }

        [JsonPropertyName("user_email")]
        public string UserEmail { get; set; }
    }
}
