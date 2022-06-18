using System;
using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Email
{
    public class SendEmailUserManagerRequest : SendEmailRequest
    {
        [Required(ErrorMessage = "Informe o código do de configuração do usuário")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Informe o código do usuário válido")]
        [Display(Name = "UserManagerId")]
        public int UserManagerId { get; set; }

        [Required(ErrorMessage = "Informe o código de ativação ou recuperação")]
        [MinLength(10, ErrorMessage = "O código deve conter {1} caracteres.")]
        [MaxLength(10, ErrorMessage = "O código deve conter {1} caracteres.")]
        [Display(Name = "ManagerCode")]
        public string ManagerCode { get; set; }
    }
}
