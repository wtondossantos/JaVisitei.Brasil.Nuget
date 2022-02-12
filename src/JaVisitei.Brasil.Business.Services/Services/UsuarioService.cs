using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;

namespace JaVisitei.Brasil.Business.Service
{
    public class UsuarioService : BaseService<Data.Entities.Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Data.Entities.Usuario Autenticacao(Data.Entities.Usuario usuario)
        {
            return _repository.Autenticacao(usuario);
        }
    }
}
