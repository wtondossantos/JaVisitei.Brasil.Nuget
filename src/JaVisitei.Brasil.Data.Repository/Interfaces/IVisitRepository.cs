using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IVisitRepository : IRepository<Visit>
    {
        Task<Visit> GetAsync(Visit request);
        Task<bool> AnyAsync(Visit request);
    }
}
