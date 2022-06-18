using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Visit
{
    public class VisitKeyRequest
    {
        [Required(ErrorMessage = "Informe o código do usuário")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Informe o código do usuário válido")]
        [Display(Name = "UserId")]
        public int UserId { get; set; }

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
