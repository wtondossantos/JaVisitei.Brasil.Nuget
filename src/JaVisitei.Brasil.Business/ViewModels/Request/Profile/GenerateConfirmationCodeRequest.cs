using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Profile
{
    public class GenerateConfirmationCodeRequest
    {
        [Required(ErrorMessage = "Informe E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
