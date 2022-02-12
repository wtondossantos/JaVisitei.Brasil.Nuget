using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Entities;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Entities.Usuario>
    {
        Entities.Usuario Autenticacao(Entities.Usuario usuario);
    }
}
