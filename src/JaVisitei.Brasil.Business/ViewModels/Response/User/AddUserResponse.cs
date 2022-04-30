using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.User
{
    public class AddUserResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("validation")]
        public ValidationResponse Validation { get; set; }
    }
}