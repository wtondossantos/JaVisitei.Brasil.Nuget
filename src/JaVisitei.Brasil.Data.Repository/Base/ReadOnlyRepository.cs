using JaVisitei.Brasil.Data.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Base
{
    public abstract class ReadOnlyRepository<T> : IReadOnlyRepository<T>, IDisposable where T : class
    {
        protected readonly DbJaVisiteiBrasilContext _context;
        protected readonly DbSet<T> _table;

        public ReadOnlyRepository(DbJaVisiteiBrasilContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return await Get(predicate, orderBy).ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return await Get(predicate, orderBy).FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await Get(predicate, null).AnyAsync();
        }

        private IQueryable<T> Get(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query;

            if (predicate is not null)
                query = _table.Where(predicate).AsNoTracking();
            else
                query = _table.AsNoTracking();

            if (orderBy is not null)
                return orderBy(query);

            return query;
        }
        public async Task<int> CountAsync()
        {
            return await _table.CountAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
