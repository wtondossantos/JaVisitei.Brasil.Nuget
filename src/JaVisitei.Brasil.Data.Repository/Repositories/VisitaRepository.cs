using JaVisitei.Brasil.Model.Models;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class VisitaRepository : BaseRepository<Visita>, IVisitaRepository
    {
        private new readonly DbJaVisiteiBrasilContext _context;

        public VisitaRepository(DbJaVisiteiBrasilContext context) : base(context)
        {
            _context = context;
        }
    }
}
