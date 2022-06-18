using AutoMapper;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Base
{
    public class Service<T> : ReadOnlyService<T>, IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository) : base(repository)
        {
            _repository = repository;
        }

        public Service(IRepository<T> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }

        public async Task<bool> InsertAsync(T entity)
        {
            return await _repository.InsertAsync(entity);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            return await _repository.DeleteAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _repository.DeleteByIdAsync(id);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            return await _repository.DeleteByIdAsync(id);
        }
    }
}
