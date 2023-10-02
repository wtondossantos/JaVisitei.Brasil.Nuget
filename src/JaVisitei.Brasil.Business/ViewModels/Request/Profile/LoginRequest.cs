using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Profile
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Informe E-mail e Senha")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe E-mail e Senha")]
        [MaxLength(255, ErrorMessage = "A senha não pode exceder {1} caracteres.")]
        [MinLength(8, ErrorMessage = "A senha não pode ser menor que {1} caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
