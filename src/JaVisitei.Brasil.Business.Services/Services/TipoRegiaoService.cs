using JaVisitei.Brasil.Model.Models;
using JaVisitei.Brasil.Service.Base;
using JaVisitei.Brasil.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;

namespace JaVisitei.Brasil.Service.Services
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
