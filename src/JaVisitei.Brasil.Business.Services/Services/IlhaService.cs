using JaVisitei.Brasil.Model.Models;
using JaVisitei.Brasil.Service.Base;
using JaVisitei.Brasil.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Service.Services
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
    }
}
