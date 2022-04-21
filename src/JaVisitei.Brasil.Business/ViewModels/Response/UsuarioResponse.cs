using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response
{
    public class UsuarioResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("sobrenome")]
        public string Sobrenome { get; set; }

        [JsonPropertyName("username")]
        public string NomeUsuario { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("senha")]
        public string Senha { get; set; }

    }
}
