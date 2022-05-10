using System.ComponentModel.DataAnnotations;

namespace JaVisitei.Brasil.Business.ViewModels.Request.User
{
    public class EditFullUserRequest : EditUserRequest
    {
        [Required(ErrorMessage = "Informe o código do tipo de perfil")]
        [Display(Name = "UserRoleId")]
        public int UserRoleId { get; set; }
    }
}
