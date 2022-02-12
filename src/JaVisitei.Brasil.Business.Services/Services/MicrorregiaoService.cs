using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Interfaces;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service
{
    public class MicrorregiaoService : BaseService<Microrregiao>, IMicrorregiaoService
    {
        private readonly IMicrorregiaoRepository _repository;

        public MicrorregiaoService(IMicrorregiaoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Microrregiao> PesquisarPorEstado(string id)
        {
            return _repository.PesquisarPorEstado(id);
        }
    }
}
