using System;

namespace JaVisitei.Brasil.Model.Models
{
    public class Visita
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoRegiao { get; set; }
        public string IdRegiao { get; set; }
        public string Cor { get; set; }
        public DateTime Data { get; set; }
    }
}
