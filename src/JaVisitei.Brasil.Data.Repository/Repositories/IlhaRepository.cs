using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Ilha> PesquisarPorEstado(string id)
        {
            return Pesquisar(x => x.Id.Substring(0, 3) == id.Substring(0, 3));
        }

        public IEnumerable<Ilha> PesquisarPorMesorregiao(string id)
        {
            var micro = _arquipelago.Pesquisar(x => x.IdMesorregiao == id).ToList();
            var model = new List<Ilha>();

            foreach (var m in micro)
            {
                var ilhas = Pesquisar(x => x.IdArquipelago == m.Id).ToList();
                model.AddRange(ilhas);
            }
            return model;
        }
    }
}
