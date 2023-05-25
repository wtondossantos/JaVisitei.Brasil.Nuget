using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using System.Collections.Generic;
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

        [JsonPropertyName("microregion")]
        public List<MicroregionResponse> Microregions { get; set; }

        [JsonPropertyName("archipelago")]
        public List<ArchipelagoResponse> Archipelagos { get; set; }
    }
}
