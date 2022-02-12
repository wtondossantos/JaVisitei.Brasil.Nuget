using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class PaisRepository : BaseRepository<Pais>, IPaisRepository
    {
        private new readonly DbJaVisiteiBrasilContext _context;

        public PaisRepository(DbJaVisiteiBrasilContext context) : base(context)
        {
            _context = context;
        }
    }
}
