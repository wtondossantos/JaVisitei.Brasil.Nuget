using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
using JaVisitei.Brasil.Business.ViewModels.Response.Country;
using JaVisitei.Brasil.Business.ViewModels.Response.Island;
using JaVisitei.Brasil.Business.ViewModels.Response.Macroregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using JaVisitei.Brasil.Business.ViewModels.Response.State;
using JaVisitei.Brasil.Caching.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.CacheModels;
using JaVisitei.Brasil.Caching.Service.Base;
using JaVisitei.Brasil.Business.Constants;
using Newtonsoft.Json;
using AutoMapper;
using JaVisitei.Brasil.Business.ViewModels.Response.Base;
using JaVisitei.Brasil.Data.Entities;

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
        private readonly IStateCachingService _stateCachingService;
        private readonly IMunicipalityCachingService _municipalityCachingService;

        public CountryCachingService(
                ICountryRepository countryRepository,
                IStateRepository stateRepository,
                IMacroregionRepository macroregionRepository,
                IMicroregionRepository microregionRepository,
                IArchipelagoRepository archipelagoRepository,
                IMunicipalityRepository municipalityRepository,
                IIslandRepository islandRepository,
                IMapper mapper,
                ICachingService cachingService,
                IStateCachingService stateCachingService,
                IMunicipalityCachingService municipalityCachingService)
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
            _stateCachingService = stateCachingService;
            _municipalityCachingService = municipalityCachingService;
        }

        public async Task<CountryCaching> GetAsync(string countryId)
        {
            var countryIdKey = $"{ countryId }_full";
            var countryCache = await _cachingService.GetAsync(countryIdKey);

            if (string.IsNullOrEmpty(countryCache))
            {
                var countryCaching = new CountryCaching
                {
                    Country = _mapper.Map<CountryResponse>(await _countryRepository.GetFirstOrDefaultAsync(x => x.Id.Equals(countryId))),
                    States = _mapper.Map<List<StateResponse>>(await _stateRepository.GetAsync(x => x.CountryId.Equals(countryId))),
                    Macroregions = _mapper.Map<List<MacroregionResponse>>(await _macroregionRepository.GetAsync()),
                    Microregions = _mapper.Map<List<MicroregionResponse>>(await _microregionRepository.GetAsync()),
                    Archipelagos = _mapper.Map<List<ArchipelagoResponse>>(await _archipelagoRepository.GetAsync()),
                    Municipalities = _mapper.Map<List<MunicipalityResponse>>(await _municipalityRepository.GetAsync()),
                    Islands = _mapper.Map<List<IslandResponse>>(await _islandRepository.GetAsync()),
                };

                await _municipalityCachingService.GetNamesByCountryAsync(countryId, countryCaching.Municipalities);

                await _cachingService.SetAsync(countryIdKey, JsonConvert.SerializeObject(countryCaching), Constant.REDIS_LOW_FREQUENCY);

                return countryCaching;
            }

            return JsonConvert.DeserializeObject<CountryCaching>(countryCache) ?? new CountryCaching();
        }

        public async Task<List<BasicResponse>> GetNamesAsync(short mapTypeId, List<CountryResponse> countries)
        {
            var countryIdKey = $"{ mapTypeId }_countries_names";
            var earthFullCache = await _cachingService.GetAsync(countryIdKey);

            if (string.IsNullOrEmpty(earthFullCache))
            {
                var countriesCaching = new List<BasicResponse>();

                if (countries is null)
                    countriesCaching = _mapper.Map<List<BasicResponse>>(await _countryRepository.GetAsync(x => x.MapTypeId.Equals(mapTypeId)));
                else
                    countriesCaching = _mapper.Map<List<BasicResponse>>(countries);

                if (countriesCaching is null || countriesCaching.Count.Equals(0)) return new List<BasicResponse>();

                await _cachingService.SetAsync(countryIdKey, JsonConvert.SerializeObject(countriesCaching), Constant.REDIS_LOW_FREQUENCY);

                return countriesCaching;
            }

            return JsonConvert.DeserializeObject<List<BasicResponse>>(earthFullCache) ?? new List<BasicResponse>();
        }

        public async Task<CountriesCaching> GetByMapTypeIdAsync(short mapTypeId)
        {
            var countriesIdKey = $"{ mapTypeId }_countries";
            var countriesCache = await _cachingService.GetAsync(countriesIdKey);

            if (string.IsNullOrEmpty(countriesCache))
            {
                var countriesCaching = new CountriesCaching
                {
                    Countries = _mapper.Map<List<CountryResponse>>(await _countryRepository.GetAsync(x => x.MapTypeId.Equals(mapTypeId)))
                };

                await GetNamesAsync(mapTypeId, countriesCaching.Countries);

                await _cachingService.SetAsync(countriesIdKey, JsonConvert.SerializeObject(countriesCaching), Constant.REDIS_LOW_FREQUENCY);

                return countriesCaching;
            }

            return JsonConvert.DeserializeObject<CountriesCaching>(countriesCache) ?? new CountriesCaching();
        }

        public async Task<CountriesCaching> GetFullByMapTypeIdAsync(short mapTypeId)
        {
            var countriesFullIdKey = $"{ mapTypeId }_earth_full";
            var countriesFullCache = await _cachingService.GetAsync(countriesFullIdKey);

            if (string.IsNullOrEmpty(countriesFullCache))
            {
                var countriesFullCaching = new CountriesCaching();
                countriesFullCaching.Countries = new List<CountryResponse>(); 

                var countries = await _countryRepository.GetAsync(x => x.MapTypeId.Equals(mapTypeId));
                var states = await _stateRepository.GetByCountryMapTypeIdAsync(mapTypeId);

                await _stateCachingService.GetNamesAsync(mapTypeId, states);

                foreach (var country in countries)
                {
                    country.States = states.Where(x => x.CountryId.Equals(country.Id)).ToList();
                    countriesFullCaching.Countries.Add(_mapper.Map<CountryResponse>(country));
                }

                await GetNamesAsync(mapTypeId, countriesFullCaching.Countries);

                await _cachingService.SetAsync(countriesFullIdKey, JsonConvert.SerializeObject(countriesFullCaching), Constant.REDIS_LOW_FREQUENCY);

                return countriesFullCaching;
            }

            return JsonConvert.DeserializeObject<CountriesCaching>(countriesFullCache) ?? new CountriesCaching();
        }
    }
}
