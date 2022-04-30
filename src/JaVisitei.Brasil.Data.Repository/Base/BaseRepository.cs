using JaVisitei.Brasil.Data.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
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
            return await _table.Where(predicate).ToListAsync();
        }

        public async Task<int> AddAsync(T entity)
        {
            var r = _table.AddAsync(entity);
            if(r.IsCompleted)
                return await _context.SaveChangesAsync();

            return -1;
        }

        public async Task<int> EditAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveAsync(Func<T, bool> predicate)
        {
            _table.Where(predicate).ToList()
                .ForEach(c => _context.Set<T>().Remove(c));

            return await _context.SaveChangesAsync();
        }
    }
}
