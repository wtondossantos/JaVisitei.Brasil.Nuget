using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class MicroregionService : BaseService<Microregion>, IMicroregionService
    {
        private readonly IMicroregionRepository _microregionRepository;

        public MicroregionService(IMicroregionRepository microregionRepository) : base(microregionRepository)
        {
            _microregionRepository = microregionRepository;
        }

        public async Task<IEnumerable<Microregion>> GetByStateAsync(string id)
        {
            return await _microregionRepository.GetByStateAsync(id);
        }
    }
}
