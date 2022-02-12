using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Helper;
using System.Linq;
using JaVisitei.Brasil.Data.Entities;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<Entities.Usuario>, IUsuarioRepository
    {
        private new readonly DbJaVisiteiBrasilContext _context;

        public UsuarioRepository(DbJaVisiteiBrasilContext context) : base(context)
        {
            _context = context;
        }

        public Entities.Usuario Autenticacao(Entities.Usuario usuario)
        {
            var senha = Encriptar.Sha256encrypt(usuario.Senha);
            
            return Pesquisar(x => x.Senha == senha && x.Email == usuario.Email).FirstOrDefault();
        }

    }
}
