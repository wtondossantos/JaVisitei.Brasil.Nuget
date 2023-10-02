using System.ComponentModel.DataAnnotations;
using System;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Email
{
    public class SendEmailRequest
    {
        [Required(ErrorMessage = "Informe o código do e-mail da mensagem")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Informe o código do usuário válido")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o e-mail do destinatário")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o nome do destinatário")]
        [DataType(DataType.Text, ErrorMessage = "Informe o nome do destinatário")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        
    }
}
