using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response
{
    public class ValidacaoResponse
    {
        [JsonPropertyName("codigo")]
        public int Codigo { get; set; }

        [JsonPropertyName("mensagem")]
        public IList<string> Mensagem { get; set; }

        [JsonPropertyName("sucesso")]
        public bool Sucesso { get; set; }
    }
}
