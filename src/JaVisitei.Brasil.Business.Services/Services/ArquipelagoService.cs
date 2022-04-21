using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service
{
    public class ArquipelagoService : BaseService<Arquipelago>, IArquipelagoService
    {
        private readonly IArquipelagoRepository _repository;

        public ArquipelagoService(IArquipelagoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Arquipelago>> PesquisarPorEstadoAsync(string id)
        {
            return await _repository.PesquisarPorEstadoAsync(id);
        }

    }
}
