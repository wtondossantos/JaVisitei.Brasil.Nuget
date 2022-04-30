using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Request.User
{
    public class EditUserRequest
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        [DataType(DataType.Text)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Informe o Nome de Usuário")]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Informe o Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a confirmação de Email")]
        [DataType(DataType.EmailAddress)]
        [Compare("Email")]
        [Display(Name = "ReEmail")]
        public string ReEmail { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "OldPassword")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "RePassword")]
        public string RePassword { get; set; }
    }
}
