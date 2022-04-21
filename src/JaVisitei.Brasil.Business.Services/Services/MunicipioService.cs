using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service
{
    public class MunicipioService : BaseService<Municipio>, IMunicipioService
    {
        private readonly IMunicipioRepository _repository;

        public MunicipioService(IMunicipioRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Municipio>> PesquisarPorEstadoAsync(string id)
        {
            return await _repository.PesquisarPorEstadoAsync(id);
        }

        public async Task<IEnumerable<Municipio>> PesquisarPorMesorregiaoAsync(string id)
        {
            return await _repository.PesquisarPorMesorregiaoAsync(id);
        }
    }
}
