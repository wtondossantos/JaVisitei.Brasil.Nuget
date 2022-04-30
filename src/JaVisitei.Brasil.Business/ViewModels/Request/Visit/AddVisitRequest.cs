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

        [Required(ErrorMessage = "Informe o Id da região")]
        [Display(Name = "RegionId")]
        public string RegionId { get; set; }

        [Required(ErrorMessage = "Informe o Id do usuário")]
        [Display(Name = "UserId")]
        public int UserId { get; set; }

        [Display(Name = "Color")]
        [DataType(DataType.Text, ErrorMessage = "Informe uma cor válida")]
        public string Color { get; set; }

        [Display(Name = "VisitDate")]
        [DataType(DataType.Date, ErrorMessage = "Informe uma data válida")]
        public DateTime VisitDate { get; set; } = DateTime.Now;

        [Display(Name = "RegistryDate")]
        [DataType(DataType.Date, ErrorMessage = "Informe uma data válida")]
        public DateTime RegistryDate { get; set; } = DateTime.Now;
    }
}
