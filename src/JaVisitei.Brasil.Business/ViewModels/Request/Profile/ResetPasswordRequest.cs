using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Profile
{
    public class ResetPasswordRequest
    {
        [Required(ErrorMessage = "Informe o código de informado no e-mail")]
        [DataType(DataType.Text)]
        [Display(Name = "ResetPasswordCode")]
        public string ResetPasswordCode { get; set; }

        [Required(ErrorMessage = "Informe o E-mail")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Informe a confirmação de Senha")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "RePassword")]
        public string RePassword { get; set; }
    }
}
