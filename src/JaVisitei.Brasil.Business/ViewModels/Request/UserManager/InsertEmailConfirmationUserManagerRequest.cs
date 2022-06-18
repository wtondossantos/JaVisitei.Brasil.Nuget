using System;
using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.UserManager
{
    public class InsertEmailConfirmationUserManagerRequest
    {
        [Required(ErrorMessage = "Informe o código do usuário")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Informe o código do usuário válido")]
        [Display(Name = "UserId")]
        public int UserId { get; set; }
    }
}
