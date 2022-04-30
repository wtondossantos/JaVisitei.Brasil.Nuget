using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Profile
{
    public class LogoutResponse
    {
        [JsonPropertyName("validation")]
        public ValidationResponse Validation { get; set; }
    }
}
