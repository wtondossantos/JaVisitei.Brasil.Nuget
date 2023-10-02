using JaVisitei.Brasil.Caching.Service.Base;
using JaVisitei.Brasil.Caching.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using AutoMapper;
using Newtonsoft.Json;
using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.ViewModels.Response.State;

namespace JaVisitei.Brasil.Caching.Service.Services
{
    public class StateCachingService : IStateCachingService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ICachingService _cachingService;

        public StateCachingService(
                IStateRepository stateRepository,
                IMapper mapper,
                ICachingService cachingService)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
            _cachingService = cachingService;
        }

        public async Task<List<StateSearchResponse>> GetNamesAsync(short mapTypeId, List<State> states)
        {
            var countryIdKey = $"{ mapTypeId }_earth_states_names";
            var earthFullCache = await _cachingService.GetAsync(countryIdKey);

            if (string.IsNullOrEmpty(earthFullCache))
            {
                if (states is null)
                    states = await _stateRepository.GetByCountryMapTypeIdAsync(mapTypeId);
                
                if (states is null || states.Count.Equals(0)) return new List<StateSearchResponse>();

                var namesCaching = _mapper.Map<List<StateSearchResponse>>(states);

                await _cachingService.SetAsync(countryIdKey, JsonConvert.SerializeObject(namesCaching), Constant.REDIS_LOW_FREQUENCY);

                return namesCaching;
            }

            return JsonConvert.DeserializeObject<List<StateSearchResponse>>(earthFullCache) ?? new List<StateSearchResponse>();
        }
    }
}
