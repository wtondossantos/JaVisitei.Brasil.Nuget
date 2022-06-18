using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Microregion
{
    public class MicroregionResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("macroregion_id")]
        public string MacroregionId { get; set; }

        [JsonPropertyName("canvas")]
        public string Canvas { get; set; }
    }
}
