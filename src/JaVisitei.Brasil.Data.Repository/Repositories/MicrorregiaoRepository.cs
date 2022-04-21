using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class MicrorregiaoRepository : BaseRepository<Microrregiao>, IMicrorregiaoRepository
    {
        private new readonly DbJaVisiteiBrasilContext _context;
        private IMesorregiaoRepository _mesorregiao;

        public MicrorregiaoRepository(DbJaVisiteiBrasilContext context) : base(context)
        {
            _context = context;
            _mesorregiao = new MesorregiaoRepository(_context);
        }

        public async Task<IEnumerable<Microrregiao>> PesquisarPorEstadoAsync(string id)
        {
            return await PesquisarAsync(x => x.Id.Substring(0, 3) == id.Substring(0, 3));
        }
    }
}
