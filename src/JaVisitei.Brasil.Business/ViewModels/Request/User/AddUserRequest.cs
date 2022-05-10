using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.User
{
    public class AddUserRequest
    {
        [Required(ErrorMessage = "Informe o Nome")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe o nome de usuário válido, sem espaço. Caracter especial permitido: (_) Underline.")]
        [Display(Name = "Username")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [DataType(DataType.Text, ErrorMessage = "Informe um sobrenome válido")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Informe o E-mail")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a confirmação de E-mail")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "ReEmail")]
        [Compare("Email")]
        public string ReEmail { get; set; }

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
