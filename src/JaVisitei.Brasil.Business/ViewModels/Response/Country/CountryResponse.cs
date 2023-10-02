using JaVisitei.Brasil.Business.ViewModels.Response.Base;
using JaVisitei.Brasil.Business.ViewModels.Response.State;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Country
{
    public class CountryResponse : BasicResponse
    {
        [JsonPropertyName("canvas")]
        public string Canvas { get; set; }

        [JsonPropertyName("states")]
        public List<StateResponse> States { get; set; }

        [JsonPropertyName("visit")]
        public VisitResponse Visit { get; set; }
    }
}
