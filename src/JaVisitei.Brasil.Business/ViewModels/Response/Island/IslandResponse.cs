using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Island
{
    public class IslandResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("archipelago_id")]
        public string ArchipelagoId { get; set; }

        [JsonPropertyName("canvas")]
        public string Canvas { get; set; }
    }
}
