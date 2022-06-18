using System;
using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.Visit
{
    public class InsertVisitRequest : VisitKeyRequest
    {
        [Display(Name = "Color")]
        [DataType(DataType.Text)]
        public string Color { get; set; }

        [Display(Name = "VisitDate")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Text, ErrorMessage = "Informe uma data válida")]
        public string VisitationDate { get; set; }
    }
}
