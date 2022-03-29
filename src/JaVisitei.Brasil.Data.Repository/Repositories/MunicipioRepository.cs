using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Municipio> PesquisarPorEstado(string id)
        {
            return Pesquisar(x => x.Id.Substring(0, 3) == id.Substring(0, 3));
        }

        public IEnumerable<Municipio> PesquisarPorMesorregiao(string id)
        {
            var micro = _microregiao.Pesquisar(x => x.Id == id).ToList();
            var model = new List<Municipio>();

            foreach (var m in micro)
            {
                var municipios = Pesquisar(x => x.IdMicrorregiao == m.Id).ToList();
                model.AddRange(municipios);
            }
            return model;
        }
    }
}
