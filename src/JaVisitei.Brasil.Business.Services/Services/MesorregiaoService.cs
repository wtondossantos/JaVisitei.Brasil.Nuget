using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Interfaces;

namespace JaVisitei.Brasil.Business.Service
{
    public class MesorregiaoService : BaseService<Mesorregiao>, IMesorregiaoService
    {
        private readonly IMesorregiaoRepository _repository;

        public MesorregiaoService(IMesorregiaoRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
