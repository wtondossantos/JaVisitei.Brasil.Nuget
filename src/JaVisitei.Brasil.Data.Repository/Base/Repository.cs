using JaVisitei.Brasil.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Base
{
    public abstract class Repository<T> : ReadOnlyRepository<T>, IRepository<T> where T : class
    {
        public Repository(DbJaVisiteiBrasilContext context) : base(context) { }

        public async Task<bool> InsertAsync(T entity)
        {
            var result = await _table.AddAsync(entity);
            if(result.State.Equals(EntityState.Added))
                return await Save();

            return false;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var result = _context.Update(entity);
            if (result.State.Equals(EntityState.Modified))
                return await Save();

            return false;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await DeleteAsync(await _table.FindAsync(id));
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            return await DeleteAsync(await _table.FindAsync(id));
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            var result = _table.Remove(entity);
            if (result.State.Equals(EntityState.Deleted))
                return await Save();

            return false;
        }

        private async Task<bool> Save()
        {
            int result = await _context.SaveChangesAsync();
            return result.Equals(1);
        }
    }
}
