using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Visit
{
    public class VisitResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("region_type_id")]
        public int RegionTypeId { get; set; }

        [JsonPropertyName("region_id")]
        public string RegionId { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("visit_date")]
        public DateOnly VisitDate { get; set; }

        [JsonPropertyName("registry_date")]
        public DateOnly RegistryDate { get; set; }
    }
}
