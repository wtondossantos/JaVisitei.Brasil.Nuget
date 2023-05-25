using JaVisitei.Brasil.Business.ViewModels.Response.State;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Country
{
    public class CountryResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("states")]
        public List<StateResponse> States { get; set; }
    }
}
