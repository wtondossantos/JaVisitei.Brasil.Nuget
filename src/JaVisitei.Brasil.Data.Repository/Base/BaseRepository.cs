using JaVisitei.Brasil.Data.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public IEnumerable<T> Pesquisar()
        {
            return _table.ToList();
        }

        public IEnumerable<T> Pesquisar(Expression<Func<T, bool>> predicate)
        {
            return _table.Where(predicate).ToList();
        }

        public void Adicionar(T entity)
        {
            _table.Add(entity);
            Salvar();
        }

        public void Alterar(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            Salvar();
        }

        public void Excluir(Func<T, bool> predicate)
        {
            _table
                .Where(predicate).ToList()
                .ForEach(c => _context.Set<T>().Remove(c));
            Salvar();
        }

        private void Salvar()
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
