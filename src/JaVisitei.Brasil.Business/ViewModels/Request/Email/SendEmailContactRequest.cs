using System;
using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Email
{
    public class SendEmailContactRequest
    {
        [Required(ErrorMessage = "Informe o código do e-mail da mensagem")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Informe o código do usuário válido")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a chave correta.")]
        [DataType(DataType.Text, ErrorMessage = "Informe a chave correta.")]
        [Display(Name = "Key")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Informe o Recaptcha correto.")]
        [DataType(DataType.Text, ErrorMessage = "Informe o Recaptcha correto.")]
        [Display(Name = "Recaptcha")]
        public string Recaptcha { get; set; }

        [Required(ErrorMessage = "Informe o código do e-mail da mensagem")]
        [MaxLength(1000, ErrorMessage = "A mensagem não pode exceder {1} caracteres.")]
        [MinLength(10, ErrorMessage = "A mensagem não pode ser menor que {1} caracteres.")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Informe o e-mail de contato.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe um assunto.")]
        [DataType(DataType.Text, ErrorMessage = "Informe um assunto.")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
    }
}
