using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Visit
{
    public class VisitKeyRequest
    {
        [Required(ErrorMessage = "Informe o Id")]
        [MaxLength(36, ErrorMessage = "O Id deve ser {1} caracteres.")]
        [MinLength(36, ErrorMessage = "O Id deve ser {1} caracteres.")]
        [DataType(DataType.Text)]
        [Display(Name = "UserId")]
        public string UserId { get; set; }

        [Required]
        [Range(1, 9, ErrorMessage = "Informe um tipo de região válido")]
        [Display(Name = "RegionTypeId")]
        public short RegionTypeId { get; set; }

        [Required(ErrorMessage = "Informe o código da região")]
        [MinLength(3, ErrorMessage = "O código da região não pode ser menor que {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "O código da região não pode exceder {1} caracteres.")]
        [Display(Name = "RegionId")]
        public string RegionId { get; set; }
    }
}
