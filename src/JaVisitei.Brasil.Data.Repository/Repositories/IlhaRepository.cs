using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class IlhaRepository : BaseRepository<Ilha>, IIlhaRepository
    {
        private new readonly DbJaVisiteiBrasilContext _context;
        private IArquipelagoRepository _arquipelago;

        public IlhaRepository(DbJaVisiteiBrasilContext context) : base(context)
        {
            _context = context;
            _arquipelago = new ArquipelagoRepository(_context);
        }

        public async Task<IEnumerable<Ilha>> PesquisarPorEstadoAsync(string id)
        {
            return await PesquisarAsync(x => x.Id.Substring(0, 3) == id.Substring(0, 3));
        }

        public async Task<IEnumerable<Ilha>> PesquisarPorMesorregiaoAsync(string id)
        {
            var micro = await _arquipelago.PesquisarAsync(x => x.IdMesorregiao == id);
            var model = new List<Ilha>();

            foreach (var m in micro)
            {
                var ilhas = await PesquisarAsync(x => x.IdArquipelago == m.Id);
                model.AddRange(ilhas);
            }
            return model;
        }
    }
}
