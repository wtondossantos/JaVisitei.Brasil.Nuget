using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JaVisitei.Brasil.Business.ViewModels.Request
{
    public class UsuarioAdicionarRequest
    {
        [Required(ErrorMessage = "Informe o Nome")]
        [DataType(DataType.Text)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Sobrenome")]
        [DataType(DataType.Text, ErrorMessage = "Informe um sobrenome válido")]
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
        [Display(Name = "ConfirmarEmail")]
        public string ConfirmarEmail { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe a confirmação de Senha")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmarSenha")]
        public string ConfirmarSenha { get; set; }
    }
}
