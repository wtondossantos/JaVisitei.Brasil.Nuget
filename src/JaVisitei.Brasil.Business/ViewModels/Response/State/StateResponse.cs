﻿using JaVisitei.Brasil.Business.ViewModels.Response.Base;
using JaVisitei.Brasil.Business.ViewModels.Response.Macroregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.State
{
    public class StateResponse : BasicResponse
    {
        [JsonPropertyName("canvas")]
        public string Canvas { get; set; }

        [JsonPropertyName("country_id")]
        public string CountryId { get; set; }

        [JsonPropertyName("macroregions")]
        public List<MacroregionResponse> Macroregions { get; set; }

        [JsonPropertyName("visit")]
        public VisitResponse Visit { get; set; }
    }
}
