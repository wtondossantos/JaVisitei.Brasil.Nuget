using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Service.Base;

namespace JaVisitei.Brasil.Service.Interfaces
{
    public interface IUsuarioService : IBaseService<Data.Entities.Usuario>
    {
        Data.Entities.Usuario Autenticacao(Data.Entities.Usuario usuario);
    }
}
