using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response
{
    public class ValidationResponse
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("mensagem")]
        public IList<string> Message { get; set; }

        [JsonPropertyName("successfully")]
        public bool Successfully { get; set; }
    }
}
