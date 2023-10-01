using JaVisitei.Brasil.Business.ViewModels.Response.Base;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Municipality
{
    public class MunicipalityResponse : BasicResponse
    {
        [JsonPropertyName("microregion_id")]
        public string MicroregionId { get; set; }

        [JsonPropertyName("canvas")]
        public string Canvas { get; set; }

        [JsonPropertyName("visit")]
        public VisitResponse Visit { get; set; }
    }
}
