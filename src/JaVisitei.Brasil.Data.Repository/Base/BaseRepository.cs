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

        public async Task<IEnumerable<T>> PesquisarAsync()
        {
            return _table.ToList();
        }

        public async Task<IEnumerable<T>> PesquisarAsync(Expression<Func<T, bool>> predicate)
        {
            return _table.Where(predicate).ToList();
        }

        public async void AdicionarAsync(T entity)
        {
            _table.Add(entity);
            SalvarAsync();
        }

        public async void AlterarAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SalvarAsync();
        }

        public async void ExcluirAsync(Func<T, bool> predicate)
        {
            _table
                .Where(predicate).ToList()
                .ForEach(c => _context.Set<T>().Remove(c));
            SalvarAsync();
        }

        private async void SalvarAsync()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                var msg = Environment.NewLine + string.Format("Property: {0} Error: {1}", dbEx.StackTrace, dbEx.Message);

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
    }
}
