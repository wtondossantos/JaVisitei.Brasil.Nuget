using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Helper;
using System.Linq;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private new readonly DbJaVisiteiBrasilContext _context;

        public UsuarioRepository(DbJaVisiteiBrasilContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Usuario> AutenticacaoAsync(Usuario usuario)
        {
            var senha = Encriptar.Sha256encrypt(usuario.Senha);
            var resultado = await PesquisarAsync(x => x.Senha == senha && x.Email == usuario.Email);

            return resultado.FirstOrDefault();    
        }

    }
}
