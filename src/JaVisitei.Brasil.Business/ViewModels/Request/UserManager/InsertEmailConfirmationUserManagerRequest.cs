using System;
using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.UserManager
{
    public class InsertEmailConfirmationUserManagerRequest
    {
        [Required(ErrorMessage = "Informe o Id")]
        [MaxLength(36, ErrorMessage = "O Id deve ser {1} caracteres.")]
        [MinLength(36, ErrorMessage = "O Id deve ser {1} caracteres.")]
        [DataType(DataType.Text)]
        [Display(Name = "UserId")]
        public string UserId { get; set; }
    }
}
