using System;
using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request
{
    public class VisitaAdicionarRequest
    {
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Informe um tipo de região válido")]
        [Display(Name = "IdTipoRegiao")]
        public int IdTipoRegiao { get; set; }

        [Required(ErrorMessage = "Informe o Id da Região")]
        [Display(Name = "IdRegiao")]
        public string IdRegiao { get; set; }

        [Display(Name = "Cor")]
        [DataType(DataType.Text, ErrorMessage = "Informe uma cor válida")]
        public string Cor { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.Date, ErrorMessage = "Informe uma data válida")]
        public DateTime Data { get; set; } = DateTime.Now;
    }
}
