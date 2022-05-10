using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Email
{
    public class SendEmailRequest
    {
        [Required(ErrorMessage = "Informe o código do e-mail da mensagem")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o código do de configuração do usuário")]
        [Display(Name = "UserManagerId")]
        public int UserManagerId { get; set; }

        [Required(ErrorMessage = "Informe o e-mail de recebimento")]
        [Display(Name = "EmailTO")]
        public string EmailTO { get; set; }

        [Required(ErrorMessage = "Informe o código de ativação ou recuperação")]
        [Display(Name = "ActivationCode")]
        public string ActivationCode { get; set; }
    }
}
