using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using JaVisitei.Brasil.Caching.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Response.Base;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class MunicipalityService : ReadOnlyService<Municipality>, IMunicipalityService
    {
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly IMunicipalityCachingService _municipalityCachingService;
        private readonly IMapper _mapper;

        public MunicipalityService(IMunicipalityRepository municipalityRepository,
            IMunicipalityCachingService municipalityCachingService,
            IMapper mapper) : base(municipalityRepository, mapper)
        {
            _municipalityRepository = municipalityRepository;
            _municipalityCachingService = municipalityCachingService;
            _mapper = mapper;
        }

        public async Task<List<BasicResponse>> GetNamesByCountryAsync(string countryId)
        {
            return await _municipalityCachingService.GetNamesByCountryAsync(countryId, null);
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
