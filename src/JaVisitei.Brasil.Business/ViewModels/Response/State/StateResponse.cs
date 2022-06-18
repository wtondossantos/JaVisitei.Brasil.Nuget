using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.State
{
    public class StateResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("canvas")]
        public string Canvas { get; set; }

        [JsonPropertyName("country_id")]
        public string CountryId { get; set; }
    }
}
