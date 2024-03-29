﻿using System;
using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Profile
{
    public class ActiveAccountRequest
    {
        [Required(ErrorMessage = "Informe o código de confirmação de e-mail")]
        [MinLength(10, ErrorMessage = "Informe o código de confirmação válido")]
        [Display(Name = "ActivationCode")]
        [DataType(DataType.Text)]
        public string ActivationCode { get; set; }
    }
}
