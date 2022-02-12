using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Service.Base;
using JaVisitei.Brasil.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;

namespace JaVisitei.Brasil.Service.Services
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
