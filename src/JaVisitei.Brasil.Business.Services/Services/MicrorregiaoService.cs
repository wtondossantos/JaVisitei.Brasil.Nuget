using JaVisitei.Brasil.Model.Models;
using JaVisitei.Brasil.Service.Base;
using JaVisitei.Brasil.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Service.Services
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
