using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Service.Base;
using JaVisitei.Brasil.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Service.Services
{
    public class ArquipelagoService : BaseService<Arquipelago>, IArquipelagoService
    {
        private readonly IArquipelagoRepository _repository;

        public ArquipelagoService(IArquipelagoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Arquipelago> PesquisarPorEstado(string id)
        {
            return _repository.PesquisarPorEstado(id);
        }

    }
}
