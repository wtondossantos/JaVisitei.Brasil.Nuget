using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Service.Base;
using JaVisitei.Brasil.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;

namespace JaVisitei.Brasil.Service.Services
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
