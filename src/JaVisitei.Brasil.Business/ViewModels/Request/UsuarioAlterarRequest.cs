using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request
{
    public class UsuarioAlterarRequest
    {
        [Required(ErrorMessage = "Informe o Nome")]
        [DataType(DataType.Text)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Sobrenome")]
        [DataType(DataType.Text)]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Informe o Nome de Usuário")]
        [DataType(DataType.Text)]
        [Display(Name = "NomeUsuario")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "Informe o Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a confirmação de Email")]
        [DataType(DataType.EmailAddress)]
        [Compare("Email")]
        [Display(Name = "ConfirmarEmail")]
        public string ConfirmarEmail { get; set; }

        [Required(ErrorMessage = "Informe a Senha Antiga")]
        [DataType(DataType.Password)]
        [Display(Name = "SenhaAntiga")]
        public string SenhaAntiga { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe a confirmação de Senha")]
        [DataType(DataType.Password)]
        [Compare("Senha")]
        [Display(Name = "ConfirmarSenha")]
        public string ConfirmarSenha { get; set; }
    }
}
