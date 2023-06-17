using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Visit
{
    public class UpdateVisitRequest : VisitKeyRequest
    {
        [Display(Name = "Color")]
        [DataType(DataType.Text)]
        public string Color { get; set; }

        [Display(Name = "Note")]
        [StringLength(255, ErrorMessage = "Informe uma nota menor que 255 caracteres.")]
        [DataType(DataType.Text)]
        public string Note { get; set; }

        [Display(Name = "VisitDate")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Text, ErrorMessage = "Informe uma data válida")]
        public string VisitationDate { get; set; }
    }
}
