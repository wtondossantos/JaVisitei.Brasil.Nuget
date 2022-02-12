using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;

namespace JaVisitei.Brasil.Business.Service
{
    public interface IUsuarioService : IBaseService<Data.Entities.Usuario>
    {
        Data.Entities.Usuario Autenticacao(Data.Entities.Usuario usuario);
    }
}
