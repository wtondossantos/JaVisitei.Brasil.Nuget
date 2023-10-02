using System.ComponentModel.DataAnnotations;
using System;
using Newtonsoft.Json;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Visit
{
    public class GetVisitUserRequest
    {
        [Required(ErrorMessage = "Informe o Id")]
        [MaxLength(36, ErrorMessage = "O Id deve ser {1} caracteres.")]
        [MinLength(36, ErrorMessage = "O Id deve ser {1} caracteres.")]
        [DataType(DataType.Text)]
        [Display(Name = "UserId")]
        public string UserId { get; set; }
    }
}
