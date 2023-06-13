
using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Recaptcha
{
    public class RecaptchaRequest
    {
        [Required(ErrorMessage = "Informe a chave correta.")]
        [DataType(DataType.Text, ErrorMessage = "Informe a chave correta.")]
        [Display(Name = "Key")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Informe o Response correto.")]
        [DataType(DataType.Text, ErrorMessage = "Informe o Response correto.")]
        [Display(Name = "Response")]
        public string Response { get; set; }
    }
}
