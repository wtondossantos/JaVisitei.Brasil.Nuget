using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service
{
    public class MunicipalityService : BaseService<Municipality>, IMunicipalityService
    {
        private readonly IMunicipalityRepository _municipalityRepository;

        public MunicipalityService(IMunicipalityRepository municipalityRepository) : base(municipalityRepository)
        {
            _municipalityRepository = municipalityRepository;
        }

        public async Task<IEnumerable<Municipality>> GetByStateAsync(string id)
        {
            return await _municipalityRepository.GetByStateAsync(id);
        }

        public async Task<IEnumerable<Municipality>> GetByMacroregionAsync(string id)
        {
            return await _municipalityRepository.GetByMacroregionAsync(id);
        }
    }
}
