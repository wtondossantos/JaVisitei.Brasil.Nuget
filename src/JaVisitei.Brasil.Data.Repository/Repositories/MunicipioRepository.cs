using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class MunicipioRepository : BaseRepository<Municipio>, IMunicipioRepository
    {
        private new readonly DbJaVisiteiBrasilContext _context;

        public MunicipioRepository(DbJaVisiteiBrasilContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Municipio> PesquisarPorEstado(string id)
        {
            return Pesquisar(x => x.Id.Substring(0, 3) == id.Substring(0, 3));
        }
    }
}
