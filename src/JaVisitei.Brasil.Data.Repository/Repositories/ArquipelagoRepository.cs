using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class ArquipelagoRepository : BaseRepository<Arquipelago>, IArquipelagoRepository
    {
        private new readonly DbJaVisiteiBrasilContext _context;
        
        public ArquipelagoRepository(DbJaVisiteiBrasilContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Arquipelago> PesquisarPorEstado(string id)
        {
            return Pesquisar(x => x.Id.Substring(0, 3) == id.Substring(0, 3));
        }
    }
}
