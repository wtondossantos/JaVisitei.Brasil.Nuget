using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Profile
{
    public class RefreshTokenRequest
    {
        [Required(ErrorMessage = "Informe E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o RToken")]
        [DataType(DataType.Text)]
        [Display(Name = "RToken")]
        public string RToken { get; set; }

        [Required(ErrorMessage = "Informe o Refresh Token")]
        [DataType(DataType.Text)]
        [Display(Name = "RefreshToken")]
        public string RefreshToken { get; set; }
    }
}
