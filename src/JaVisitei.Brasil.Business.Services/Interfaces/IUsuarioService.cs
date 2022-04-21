using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Business.ViewModels.Request;
using JaVisitei.Brasil.Business.ViewModels.Response;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IUsuarioService : IBaseService<Usuario>
    {
        Task<LoginResponse> LoginAsync(LoginRequest usuario);
        Task<ValidacaoResponse> LogoutAsync();
    }
}
