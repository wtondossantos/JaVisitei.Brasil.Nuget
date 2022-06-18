using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Country
{
    public class CountryResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
