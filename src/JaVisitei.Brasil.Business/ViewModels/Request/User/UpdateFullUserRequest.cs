using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.User
{
    public class UpdateFullUserRequest : UpdateUserRequest
    {
        [Required(ErrorMessage = "Informe o código do tipo de perfil")]
        [Range(1, 9, ErrorMessage = "Informe o código do tipo de perfil valido")]
        [Display(Name = "UserRoleId")]
        public int UserRoleId { get; set; }
    }
}
