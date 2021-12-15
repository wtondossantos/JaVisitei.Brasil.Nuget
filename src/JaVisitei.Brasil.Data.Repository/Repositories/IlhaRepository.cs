using JaVisitei.Brasil.Model.Models;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class IlhaRepository : BaseRepository<Ilha>, IIlhaRepository
    {
        private new readonly DbJaVisiteiBrasilContext _context;

        public IlhaRepository(DbJaVisiteiBrasilContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Ilha> PesquisarPorEstado(string id)
        {
            return Pesquisar(x => x.Id.Substring(0, 3) == id.Substring(0, 3));
        }
    }
}
