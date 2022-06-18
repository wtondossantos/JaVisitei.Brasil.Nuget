using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Macroregion
{
    public class MacroregionResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("state_id")]
        public string StateId { get; set; }

        [JsonPropertyName("canvas")]
        public string Canvas { get; set; }
    }
}
