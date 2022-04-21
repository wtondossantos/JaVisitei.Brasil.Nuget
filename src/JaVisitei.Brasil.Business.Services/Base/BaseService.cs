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

        public async void AdicionarAsync(T entity)
        {
            _repository.AdicionarAsync(entity);
        }

        public async void AlterarAsync(T entity)
        {
            _repository.AlterarAsync(entity);
        }

        public async void ExcluirAsync(Func<T, bool> predicate)
        {
            _repository.ExcluirAsync(predicate);
        }

        public async Task<IEnumerable<T>> PesquisarAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.PesquisarAsync(predicate);
        }

        public async Task<IEnumerable<T>> PesquisarAsync()
        {
            return await _repository.PesquisarAsync();
        }
    }
}
