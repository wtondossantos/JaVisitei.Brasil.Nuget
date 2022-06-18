using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Base
{
    public interface IService<T> : IReadOnlyService<T> where T : class
    {
        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> DeleteByIdAsync(string id);
    }
}
