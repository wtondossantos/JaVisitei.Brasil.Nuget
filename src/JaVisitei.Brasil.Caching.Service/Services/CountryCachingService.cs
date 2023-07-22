using JaVisitei.Brasil.Business.CacheModels;
using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
using JaVisitei.Brasil.Business.ViewModels.Response.Country;
using JaVisitei.Brasil.Business.ViewModels.Response.Island;
using JaVisitei.Brasil.Business.ViewModels.Response.Macroregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using JaVisitei.Brasil.Business.ViewModels.Response.State;
using Newtonsoft.Json;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Caching.Service.Base;
using AutoMapper;
using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Caching.Service.Interfaces;

namespace JaVisitei.Brasil.Caching.Service.Services
{
    public class CountryCachingService : ICountryCachingService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IMacroregionRepository _macroregionRepository;
        private readonly IMicroregionRepository _microregionRepository;
        private readonly IArchipelagoRepository _archipelagoRepository;
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly IIslandRepository _islandRepository;
        private readonly IMapper _mapper;
        private readonly ICachingService _cachingService;

        public CountryCachingService(
                ICountryRepository countryRepository,
                IStateRepository stateRepository,
                IMacroregionRepository macroregionRepository,
                IMicroregionRepository microregionRepository,
                IArchipelagoRepository archipelagoRepository,
                IMunicipalityRepository municipalityRepository,
                IIslandRepository islandRepository,
                IMapper mapper,
                ICachingService cachingService)
        {
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _archipelagoRepository = archipelagoRepository;
            _macroregionRepository = macroregionRepository;
            _microregionRepository = microregionRepository;
            _municipalityRepository = municipalityRepository;
            _islandRepository = islandRepository;
            _mapper = mapper;
            _cachingService = cachingService;
        }

        public async Task<CountryCaching> GetAsync(string countryId)
        {
            var countryIdKey = $"{ countryId }_full";
            var countryCache = await _cachingService.GetAsync(countryIdKey);

            if (string.IsNullOrEmpty(countryCache))
            {
                var countryCaching = new CountryCaching
                {
                    Country = _mapper.Map<CountryResponse>(await _countryRepository.GetByIdAsync(countryId)),
                    States = _mapper.Map<List<StateResponse>>(await _stateRepository.GetAsync(x => x.CountryId.Equals(countryId))),
                    Macroregions = _mapper.Map<List<MacroregionResponse>>(await _macroregionRepository.GetAsync()),
                    Microregions = _mapper.Map<List<MicroregionResponse>>(await _microregionRepository.GetAsync()),
                    Archipelagos = _mapper.Map<List<ArchipelagoResponse>>(await _archipelagoRepository.GetAsync()),
                    Municipalities = _mapper.Map<List<MunicipalityResponse>>(await _municipalityRepository.GetAsync()),
                    Islands = _mapper.Map<List<IslandResponse>>(await _islandRepository.GetAsync()),
                };

                await _cachingService.SetAsync(countryIdKey, JsonConvert.SerializeObject(countryCaching), Constant.REDIS_LOW_FREQUENCY);

                return countryCaching;
            }

            return JsonConvert.DeserializeObject<CountryCaching>(countryCache) ?? new CountryCaching();
        }
    }
}
