using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Base
{
    public interface IReadOnlyService<T>  where T : class
    {
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<IEnumerable<M>> GetAsync<M>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<M> GetFirstOrDefaultAsync<M>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<T> GetByIdAsync(string id);
        Task<M> GetByIdAsync<M>(string id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);
        Task<int> CountAsync();
    }
}
