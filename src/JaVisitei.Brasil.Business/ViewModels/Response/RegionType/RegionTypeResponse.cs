using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.RegionType
{
    public class RegionTypeResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
