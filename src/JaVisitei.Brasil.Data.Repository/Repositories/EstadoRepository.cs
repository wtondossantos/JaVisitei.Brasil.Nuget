using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class EstadoRepository : BaseRepository<Estado>, IEstadoRepository
    {
        private new readonly DbJaVisiteiBrasilContext _context;

        public EstadoRepository(DbJaVisiteiBrasilContext context) : base(context)
        {
            _context = context;
        }
    }
}
