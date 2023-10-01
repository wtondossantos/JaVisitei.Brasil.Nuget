using JaVisitei.Brasil.Business.ViewModels.Response.Base;
using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Microregion
{
    public class MicroregionResponse : BasicResponse
    {
        [JsonPropertyName("macroregion_id")]
        public string MacroregionId { get; set; }

        [JsonPropertyName("canvas")]
        public string Canvas { get; set; }

        [JsonPropertyName("municipalities")]
        public List<MunicipalityResponse> Municipalities { get; set; }

        [JsonPropertyName("visit")]
        public VisitResponse Visit { get; set; }
    }
}
