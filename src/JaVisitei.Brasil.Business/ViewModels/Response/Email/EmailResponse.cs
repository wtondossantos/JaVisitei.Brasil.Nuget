using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Email
{
    public class EmailResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}
