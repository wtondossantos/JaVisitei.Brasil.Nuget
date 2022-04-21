using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service
{
    public class IlhaService : BaseService<Ilha>, IIlhaService
    {
        private readonly IIlhaRepository _repository;

        public IlhaService(IIlhaRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Ilha>> PesquisarPorEstadoAsync(string id)
        {
            return await _repository.PesquisarPorEstadoAsync(id);
        }

        public async Task<IEnumerable<Ilha>> PesquisarPorMesorregiaoAsync(string id)
        {
            return await _repository.PesquisarPorMesorregiaoAsync(id);
        }
    }
}
