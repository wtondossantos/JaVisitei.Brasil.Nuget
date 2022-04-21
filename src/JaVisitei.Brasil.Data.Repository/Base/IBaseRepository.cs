using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> PesquisarAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> PesquisarAsync();
        void AdicionarAsync(T entity);
        void AlterarAsync(T entity);
        void ExcluirAsync(Func<T, bool> predicate);
    }
}
