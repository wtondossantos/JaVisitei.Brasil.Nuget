using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Interfaces;

namespace JaVisitei.Brasil.Business.Service
{
    public class PaisService : BaseService<Pais>, IPaisService
    {
        private readonly IPaisRepository _repository;

        public PaisService(IPaisRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
