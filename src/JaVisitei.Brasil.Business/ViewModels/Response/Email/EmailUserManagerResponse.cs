using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Email
{
    public class EmailUserManagerResponse : EmailResponse
    {
        [JsonPropertyName("user_manager_id")]
        public int UserManagerId { get; set; }
    }
}
