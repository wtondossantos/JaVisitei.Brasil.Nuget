using JaVisitei.Brasil.Business.ViewModels.Response.Base;
using JaVisitei.Brasil.Business.ViewModels.Response.Island;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Archipelago
{
    public class ArchipelagoResponse : BasicResponse
    {
        [JsonPropertyName("macroregion_id")]
        public string MacroregionId { get; set; }

        [JsonPropertyName("islands")]
        public List<IslandResponse> Islands { get; set; }

        [JsonPropertyName("visit")]
        public VisitResponse Visit { get; set; }
    }
}
