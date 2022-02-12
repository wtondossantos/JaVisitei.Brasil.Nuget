using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Interfaces;

namespace JaVisitei.Brasil.Business.Service
{
    public class VisitaService : BaseService<Visita>, IVisitaService
    {
        private readonly IVisitaRepository _repository;

        public VisitaService(IVisitaRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
