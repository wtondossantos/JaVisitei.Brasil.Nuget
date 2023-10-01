using JaVisitei.Brasil.Business.ViewModels.Response.Country;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using JaVisitei.Brasil.Business.ViewModels.Response.State;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Caching.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using JaVisitei.Brasil.Business.ViewModels.Response.Base;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class CountryService : ReadOnlyService<Country>, ICountryService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IMapper _mapper;
        private readonly ICountryCachingService _countryCachingService;

        public CountryService(
                ICountryRepository countryRepository,
                IVisitRepository visitRepository,
                IMapper mapper,
                ICountryCachingService countryCachingService) : base(countryRepository, mapper) {
            _visitRepository = visitRepository;
            _mapper = mapper;
            _countryCachingService = countryCachingService;
        }

        public async Task<List<CountryResponse>> GetByMapTypeIdAsync(short mapTypeId)
        {
            var countryCaching = await _countryCachingService.GetByMapTypeIdAsync(mapTypeId);
            if (countryCaching is null) return null;

            return countryCaching.Countries;
        }

        public async Task<List<CountryResponse>> GetFullByMapTypeIdAsync(short mapTypeId)
        {
            var countriesfullCaching = await _countryCachingService.GetFullByMapTypeIdAsync(mapTypeId);
            if (countriesfullCaching is null) return null;

            return countriesfullCaching.Countries;
        }

        public async Task<List<CountryResponse>> GetByMapTypeIdAndUserIdAsync(short mapTypeId, string userId)
        {
            var countriesfullCaching = await _countryCachingService.GetByMapTypeIdAsync(mapTypeId);
            if (countriesfullCaching is null) return null;

            List<VisitResponse> visits = null;
            if (!string.IsNullOrEmpty(userId))
                visits = _mapper.Map<List<VisitResponse>>(await _visitRepository.GetAsync(x => x.UserId.Equals(userId)));

            if (visits is null)
                return countriesfullCaching.Countries;

            var countries = new List<CountryResponse>();
            foreach (var country in countriesfullCaching.Countries)
            {
                var visit = visits.Where(x => x.RegionId.Equals(country.Id)).FirstOrDefault();
                if (visit != null)
                {
                    country.Visit = visit;
                    visits.Remove(visit);
                }

                countries.Add(country);
            }

            return countries;
        }

        public async Task<List<CountryResponse>> GetFullByMapTypeIdAndUserIdAsync(short mapTypeId, string userId)
        {
            try
            {
                var countriesCaching = await _countryCachingService.GetFullByMapTypeIdAsync(mapTypeId);
                if (countriesCaching is null) return null;

                List<VisitResponse> visits = null;
                if (!string.IsNullOrEmpty(userId))
                    visits = _mapper.Map<List<VisitResponse>>(await _visitRepository.GetAsync(x => x.UserId.Equals(userId)));

                if (visits is null)
                    return countriesCaching.Countries;

                var countries = new List<CountryResponse>();
                foreach (var country in countriesCaching.Countries)
                {
                    var states = country.States;
                    country.States = new List<StateResponse>();

                    foreach (var state in states)
                    {
                        var visit = visits.Where(x => x.RegionId.Equals(state.Id)).FirstOrDefault();
                        if (visit != null)
                        {
                            state.Visit = visit;
                            visits.Remove(visit);
                        }

                        country.States.Add(state);
                    }

                    countries.Add(country);
                }

                return countries;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CountryResponse> GetFullByIdAsync(string countryId)
        {
            var countryCaching = await _countryCachingService.GetAsync(countryId);
            if (countryCaching is null) return null;

            return countryCaching.Country;
        }

        public async Task<CountryResponse> GetFullByIdAndUserIdAsync(string countryId, string userId)
        {
            try
            {
                var countryCaching = await _countryCachingService.GetAsync(countryId);
                if (countryCaching is null) return null;

                List<VisitResponse> visits = null;
                if(!string.IsNullOrEmpty(userId))
                    visits = _mapper.Map<List<VisitResponse>>(await _visitRepository.GetAsync(x => x.UserId.Equals(userId)));

                if (visits is null)
                    return countryCaching.Country;

                var country = new CountryResponse();
                country.States = new List<StateResponse>();
                foreach (var state in countryCaching.States)
                {
                    var listMacroregion = countryCaching.Macroregions.Where(x => x.StateId.Equals(state.Id));
                    foreach (var macro in listMacroregion)
                    {
                        var listMicroregion = countryCaching.Microregions.Where(x => x.MacroregionId.Equals(macro.Id));
                        foreach (var micro in listMicroregion)
                        {
                            var listMunicipality = countryCaching.Municipalities.Where(x => x.MicroregionId.Equals(micro.Id));
                            if (visits is null || visits.Count.Equals(0))
                                micro.Municipalities = listMunicipality.ToList();
                            else 
                            {
                                foreach (var municipality in listMunicipality) 
                                {
                                    var visit = visits.Where(x => x.RegionId.Equals(municipality.Id)).FirstOrDefault();
                                    if (visit != null)
                                    {
                                        municipality.Visit = visit;
                                        visits.Remove(visit);
                                    }

                                    micro.Municipalities.Add(municipality);
                                }
                            }
                            macro.Microregions.Add(micro);
                        }

                        var listArchipelago = countryCaching.Archipelagos.Where(x => x.MacroregionId.Equals(macro.Id));
                        foreach (var archipelago in listArchipelago)
                        {
                            var listIsland = countryCaching.Islands.Where(x => x.ArchipelagoId.Equals(archipelago.Id));
                            if (visits is null || visits.Count.Equals(0))
                                archipelago.Islands = listIsland.ToList();
                            else
                            {
                                foreach (var island in listIsland)
                                {
                                    var visit = visits.Where(x => x.RegionId.Equals(island.Id)).FirstOrDefault();
                                    if (visit != null)
                                    {
                                        island.Visit = visit;
                                        visits.Remove(visit);
                                    }

                                    archipelago.Islands.Add(island);
                                }
                            }
                            macro.Archipelagos.Add(archipelago);
                        }
                        state.Macroregions.Add(macro);
                    }

                    country.States.Add(state);
                }

                return country;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<BasicResponse>> GetNamesAsync(short mapTypeId)
        {
            return await _countryCachingService.GetNamesAsync(mapTypeId, null);
        }
    }
}
