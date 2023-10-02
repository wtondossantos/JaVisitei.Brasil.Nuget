using System;
using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Profile
{
    public class ResetPasswordRequest
    {
        [Required(ErrorMessage = "Informe o código de informado no e-mail")]
        [DataType(DataType.Text)]
        [MaxLength(10, ErrorMessage = "Informe o código de confirmação válido")]
        [Display(Name = "ResetPasswordCode")]
        public string ResetPasswordCode { get; set; }

        [Required(ErrorMessage = "Informe o E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-amil válido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [MaxLength(255, ErrorMessage = "A senha não pode exceder {1} caracteres.")]
        [MinLength(8, ErrorMessage = "A senha não pode ser menor que {1} caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Informe a confirmação de Senha")]
        [DataType(DataType.Password)]
        [MaxLength(255, ErrorMessage = "A senha não pode exceder {1} caracteres.")]
        [MinLength(8, ErrorMessage = "A senha não pode ser menor que {1} caracteres.")]
        [Compare("Password", ErrorMessage = "Senha e confirmação de senha são diferentes")]
        [Display(Name = "RePassword")]
        public string RePassword { get; set; }
    }
}
