using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Interfaces;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service
{
    public class IlhaService : BaseService<Ilha>, IIlhaService
    {
        private readonly IIlhaRepository _repository;

        public IlhaService(IIlhaRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Ilha> PesquisarPorEstado(string id)
        {
            return _repository.PesquisarPorEstado(id);
        }

        public IEnumerable<Ilha> PesquisarPorMesorregiao(string id)
        {
            return _repository.PesquisarPorMesorregiao(id);
        }
    }
}
