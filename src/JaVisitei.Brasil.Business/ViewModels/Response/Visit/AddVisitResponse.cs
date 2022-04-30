using System;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Visit
{
    public class AddVisitResponse
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
        public DateTime VisitDate { get; set; }

        [JsonPropertyName("registry_date")]
        public DateTime RegistryDate { get; set; }

        [JsonPropertyName("validation")]
        public ValidationResponse Validation { get; set; }
    }
}
