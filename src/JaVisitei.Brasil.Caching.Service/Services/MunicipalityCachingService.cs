using AutoMapper;
using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.ViewModels.Response.Base;
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

        public async Task<List<BasicResponse>> GetNamesByCountryAsync(string countryId, List<MunicipalityResponse> municipalities)
        {
            var countryIdKey = $"{ countryId }_municipality";
            var municipalityCache = await _cachingService.GetAsync(countryIdKey);
            
            if (string.IsNullOrEmpty(municipalityCache))
            {
                var municipalitiesCaching = new List<BasicResponse>();

                if (municipalities is null)
                    municipalitiesCaching = _mapper.Map<List<BasicResponse>>(await _municipalityRepository.GetAsync());
                else
                    municipalitiesCaching = _mapper.Map<List<BasicResponse>>(municipalities);

                if (municipalitiesCaching is null || municipalitiesCaching.Count.Equals(0)) return new List<BasicResponse>();

                await _cachingService.SetAsync(countryIdKey, JsonConvert.SerializeObject(municipalitiesCaching), Constant.REDIS_LOW_FREQUENCY);

                return municipalitiesCaching;
            }

            return JsonConvert.DeserializeObject<List<BasicResponse>>(municipalityCache) ?? new List<BasicResponse>();
        }

    }
}
