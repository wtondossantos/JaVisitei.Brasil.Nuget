using JaVisitei.Brasil.Business.ViewModels.Response.Base;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.State
{
    public class StateSearchResponse : BasicResponse
    {
        [JsonPropertyName("name_country")]
        public string NameCountry { get; set; }
    }
}
