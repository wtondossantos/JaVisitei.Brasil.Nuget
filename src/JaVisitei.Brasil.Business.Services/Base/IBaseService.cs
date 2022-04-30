using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Base
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        Task<int> EditAsync(T entity);
        Task<int> RemoveAsync(Func<T, bool> predicate);
    }
}
