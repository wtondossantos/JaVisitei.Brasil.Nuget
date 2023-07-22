using AutoMapper;
using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using JaVisitei.Brasil.Caching.Service.Base;
using JaVisitei.Brasil.Caching.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using Newtonsoft.Json;

namespace JaVisitei.Brasil.Caching.Service.Services
{
    public class MunicipalityCachingService : IMunicipalityCachingService
    {
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly IMapper _mapper;
        private readonly ICachingService _cachingService;

        public MunicipalityCachingService(
                IMunicipalityRepository municipalityRepository,
                IMapper mapper,
                ICachingService cachingService)
        {
            _municipalityRepository = municipalityRepository;
            _mapper = mapper;
            _cachingService = cachingService;
        }

        public async Task<List<MunicipalityBasicResponse>> GetByCountryIdAsync(string countryId)
        {
            var countryIdKey = $"{ countryId }_municipality";
            var municipalityCache = await _cachingService.GetAsync(countryIdKey);
            
            if (string.IsNullOrEmpty(municipalityCache))
            {
                var municipalities = await _municipalityRepository.GetAsync();
                if (municipalities is null) return new List<MunicipalityBasicResponse>();

                var municipalitiesCaching = _mapper.Map<List<MunicipalityBasicResponse>>(municipalities);

                await _cachingService.SetAsync(countryIdKey, JsonConvert.SerializeObject(municipalitiesCaching), Constant.REDIS_LOW_FREQUENCY);

                return municipalitiesCaching;
            }

            return JsonConvert.DeserializeObject<List<MunicipalityBasicResponse>>(municipalityCache) ?? new List<MunicipalityBasicResponse>();
        }

    }
}
