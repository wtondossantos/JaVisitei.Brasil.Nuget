using System;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Base
{
    public interface IRepository<T> : IReadOnlyRepository<T>, IDisposable where T : class
    {
        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> DeleteByIdAsync(string id);
    }
}
