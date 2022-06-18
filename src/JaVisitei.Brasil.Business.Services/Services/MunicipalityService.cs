using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class MunicipalityService : ReadOnlyService<Municipality>, IMunicipalityService
    {
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly IMapper _mapper;

        public MunicipalityService(IMunicipalityRepository municipalityRepository, 
            IMapper mapper) : base(municipalityRepository, mapper)
        {
            _municipalityRepository = municipalityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<M>> GetByStateAsync<M>(string stateId)
        {
            var items = await _municipalityRepository.GetByStateAsync(stateId);
            return items is null || !items.Any() ? default : _mapper.Map<IEnumerable<M>>(items);
        }

        public async Task<IEnumerable<M>> GetByMacroregionAsync<M>(string macroregionId)
        {
            var items = await _municipalityRepository.GetByMacroregionAsync(macroregionId);
            return items is null || !items.Any() ? default : _mapper.Map<IEnumerable<M>>(items);
        }
    }
}
