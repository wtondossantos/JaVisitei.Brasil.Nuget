using JaVisitei.Brasil.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Base
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.GetAsync(predicate);
        }

        public async Task<int> AddAsync(T entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task<int> EditAsync(T entity)
        {
            return await _repository.EditAsync(entity);
        }

        public async Task<int> RemoveAsync(Func<T, bool> predicate)
        {
            return await _repository.RemoveAsync(predicate);
        }
    }
}
