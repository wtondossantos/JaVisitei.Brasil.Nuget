using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.User
{
    public class EditUserResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
        
        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("validation")]
        public ValidationResponse Validation { get; set; }

    }
}
