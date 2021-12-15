using JaVisitei.Brasil.Model.Models;
using JaVisitei.Brasil.Service.Base;

namespace JaVisitei.Brasil.Service.Interfaces
{
    public interface IUsuarioService : IBaseService<Usuario>
    {
        Usuario Autenticacao(Usuario usuario);
    }
}
