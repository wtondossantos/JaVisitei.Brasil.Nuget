using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Service.Base;
using JaVisitei.Brasil.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;

namespace JaVisitei.Brasil.Service.Services
{
    public class EstadoService : BaseService<Estado>, IEstadoService
    {
        private readonly IEstadoRepository _repository;

        public EstadoService(IEstadoRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
