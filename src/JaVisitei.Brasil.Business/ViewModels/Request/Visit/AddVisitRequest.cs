using System;
using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Visit
{
    public class AddVisitRequest
    {
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Informe um tipo de região válido")]
        [Display(Name = "RegionTypeId")]
        public int RegionTypeId { get; set; }

        [Required(ErrorMessage = "Informe o código da região")]
        [Display(Name = "RegionId")]
        public string RegionId { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Informe o código do usuário válido")]
        [Required(ErrorMessage = "Informe o código do usuário")]
        [Display(Name = "UserId")]
        public int UserId { get; set; }

        [Display(Name = "Color")]
        [DataType(DataType.Text)]
        public string Color { get; set; }

        [Display(Name = "VisitDate")]
        [DataType(DataType.Date, ErrorMessage = "Informe uma data válida")]
        public DateOnly VisitDate { get; set; }
    }
}
