using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System;

namespace JaVisitei.Brasil.Business.Service.Base
{
    public class ReadOnlyService<T> : IReadOnlyService<T> where T : class
    {
        private readonly IReadOnlyRepository<T> _repository;
        private readonly IMapper _mapper;

        public ReadOnlyService(IReadOnlyRepository<T> repository)
        {
            _repository = repository;
        }

        public ReadOnlyService(IReadOnlyRepository<T> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return await _repository.GetAsync(predicate, orderBy);
        }
        public async Task<IEnumerable<M>> GetAsync<M>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var items = await _repository.GetAsync(predicate, orderBy);
            return items is null || !items.Any() ? default : _mapper.Map<IEnumerable<M>>(items);
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            return await _repository.GetFirstOrDefaultAsync(predicate, orderBy);
        }
        public async Task<M> GetFirstOrDefaultAsync<M>(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var item = await _repository.GetFirstOrDefaultAsync(predicate, orderBy);
            return item is null ? default : _mapper.Map<M>(item);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<M> GetByIdAsync<M>(string id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item is null ? default : _mapper.Map<M>(item);
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
        {
            return _repository.AnyAsync(predicate);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
