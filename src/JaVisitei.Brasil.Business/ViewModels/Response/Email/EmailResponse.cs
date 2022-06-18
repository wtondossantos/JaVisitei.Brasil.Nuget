using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Email
{
    public class EmailResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
