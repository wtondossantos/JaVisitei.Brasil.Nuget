using JaVisitei.Brasil.Data.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : class
    {
        protected readonly DbJaVisiteiBrasilContext _context;
        private DbSet<T> _table;

        public BaseRepository(DbJaVisiteiBrasilContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _table.Where(predicate).ToArrayAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _table.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, Object>> orderby)
        {
            return await _table.Where(predicate).OrderBy(orderby).FirstOrDefaultAsync();
        }

        public async Task<bool> AddAsync(T entity)
        {
            int result = 0;
            var r = _table.AddAsync(entity);
            if(r.IsCompleted)
                result = await _context.SaveChangesAsync();

            return result.Equals(1);
        }

        public async Task<bool> EditAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result.Equals(1);
        }

        public async Task<bool> RemoveAsync(Func<T, bool> predicate)
        {
            _table.Where(predicate).ToList()
                .ForEach(c => _context.Set<T>().Remove(c));
            int result = await _context.SaveChangesAsync();

            return result.Equals(1);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
