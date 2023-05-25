using JaVisitei.Brasil.Business.ViewModels.Response.Island;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Archipelago
{
    public class ArchipelagoResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("macroregion_id")]
        public string MacroregionId { get; set; }

        [JsonPropertyName("island")]
        public List<IslandResponse> Islands { get; set; }
    }
}
