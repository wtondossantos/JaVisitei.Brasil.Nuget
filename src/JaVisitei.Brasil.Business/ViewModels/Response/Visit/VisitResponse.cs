using Newtonsoft.Json;
using System;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Visit
{
    public class VisitResponse
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("region_type_id")]
        public int RegionTypeId { get; set; }

        [JsonProperty("region_id")]
        public string RegionId { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("visit_date")]
        public DateTime? VisitDate { get; set; }

        [JsonProperty("registry_date")]
        public DateTime RegistryDate { get; set; }
    }
}
