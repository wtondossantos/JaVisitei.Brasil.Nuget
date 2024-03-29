﻿using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.User
{
    public class InsertUserRequest
    {
        [Required(ErrorMessage = "Informe o Nome")]
        [MaxLength(50, ErrorMessage = "O nome não pode exceder {1} caracteres.")]
        [MinLength(3, ErrorMessage = "O nome não pode ser menor que {1} caracteres.")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.Text, ErrorMessage = "Informe um sobrenome válido")]
        [MaxLength(200, ErrorMessage = "O sobrenome não pode exceder {1} caracteres.")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Informe o nome de usuário válido, sem espaço. Caracter especial permitido: (_) Underline.")]
        [MaxLength(25, ErrorMessage = "O nome de usuário não pode exceder {1} caracteres.")]
        [Display(Name = "Username")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Informe o E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-amil válido")]
        [MaxLength(200, ErrorMessage = "O e-mail não pode exceder {1} caracteres.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a confirmação de E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-amil válido")]
        [MaxLength(200, ErrorMessage = "O e-mail não pode exceder {1} caracteres.")]
        [Display(Name = "ReEmail")]
        [Compare("Email", ErrorMessage = "E-mail e confirmação de e-mail são diferentes")]
        public string ReEmail { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [MaxLength(50, ErrorMessage = "A senha não pode exceder {1} caracteres.")]
        [MinLength(8, ErrorMessage = "A senha não pode ser menor que {1} caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Informe a confirmação de Senha")]
        [MaxLength(50, ErrorMessage = "A senha não pode exceder {1} caracteres.")]
        [MinLength(8, ErrorMessage = "A senha não pode ser menor que {1} caracteres.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Senha e confirmação de senha são diferentes")]
        [Display(Name = "RePassword")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "Informe a chave correta.")]
        [DataType(DataType.Text, ErrorMessage = "Informe a chave correta.")]
        [Display(Name = "Key")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Informe o Recaptcha correto.")]
        [DataType(DataType.Text, ErrorMessage = "Informe o Recaptcha correto.")]
        [Display(Name = "Recaptcha")]
        public string Recaptcha { get; set; }

        [Required(ErrorMessage = "Informe se deseja receber Newsletter.")]
        [Display(Name = "Newsletter")]
        public bool Newsletter { get; set; }
    }
}
