using System;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.Profile
{
    public class ActivationResponse
    {
        [JsonPropertyName("active")]
        public bool Active { get; set; }
    }
}
