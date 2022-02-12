using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;

namespace JaVisitei.Brasil.Business.Service
{
    public class TipoRegiaoService : BaseService<TipoRegiao>, ITipoRegiaoService
    {
        private readonly ITipoRegiaoRepository _repository;

        public TipoRegiaoService(ITipoRegiaoRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
