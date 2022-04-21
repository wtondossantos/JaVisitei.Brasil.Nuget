using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class MunicipioRepository : BaseRepository<Municipio>, IMunicipioRepository
    {
        private new readonly DbJaVisiteiBrasilContext _context;
        private IMicrorregiaoRepository _microregiao;

        public MunicipioRepository(DbJaVisiteiBrasilContext context) : base(context)
        {
            _context = context;
            _microregiao = new MicrorregiaoRepository(_context);
        }

        public async Task<IEnumerable<Municipio>> PesquisarPorEstadoAsync(string id)
        {
            return await PesquisarAsync(x => x.Id.Substring(0, 3) == id.Substring(0, 3));
        }

        public async Task<IEnumerable<Municipio>> PesquisarPorMesorregiaoAsync(string id)
        {
            var micro = await _microregiao.PesquisarAsync(x => x.IdMesorregiao == id);
            var model = new List<Municipio>();

            foreach (var m in micro)
            {
                var municipios = await PesquisarAsync(x => x.IdMicrorregiao == m.Id);
                model.AddRange(municipios);
            }
            return model;
        }
    }
}
