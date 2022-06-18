using System.ComponentModel.DataAnnotations;
using System;
using Newtonsoft.Json;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Visit
{
    public class GetVisitUserRequest
    {
        [Range(1, Int32.MaxValue, ErrorMessage = "Informe o código do usuário válido")]
        [Required(ErrorMessage = "Informe o código do usuário")]
        [Display(Name = "UserId")]
        public int UserId { get; set; }
    }
}
