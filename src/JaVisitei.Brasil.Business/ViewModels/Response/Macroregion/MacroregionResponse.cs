using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
using JaVisitei.Brasil.Business.ViewModels.Response.Base;
using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Macroregion
{
    public class MacroregionResponse : BasicResponse
    {
        [JsonPropertyName("state_id")]
        public string StateId { get; set; }

        [JsonPropertyName("canvas")]
        public string Canvas { get; set; }

        [JsonPropertyName("microregions")]
        public List<MicroregionResponse> Microregions { get; set; }

        [JsonPropertyName("archipelagos")]
        public List<ArchipelagoResponse> Archipelagos { get; set; }

        [JsonPropertyName("visit")]
        public VisitResponse Visit { get; set; }
    }
}
