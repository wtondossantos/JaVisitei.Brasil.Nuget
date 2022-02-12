using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Service.Base;
using JaVisitei.Brasil.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Data.Entities;

namespace JaVisitei.Brasil.Service.Services
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
