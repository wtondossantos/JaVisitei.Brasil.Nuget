using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Interfaces;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service
{
    public class MunicipioService : BaseService<Municipio>, IMunicipioService
    {
        private readonly IMunicipioRepository _repository;

        public MunicipioService(IMunicipioRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Municipio> PesquisarPorEstado(string id)
        {
            return _repository.PesquisarPorEstado(id);
        }
    }
}
