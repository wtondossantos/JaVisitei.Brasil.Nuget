using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using JaVisitei.Brasil.Business.ViewModels.Response.Country;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using JaVisitei.Brasil.Business.CacheModels;
using JaVisitei.Brasil.Caching.Service.Interfaces;

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

        public async Task<CountryResponse> GetFullByIdAsync(string countryId) 
        {
            return await GetFullByIdAndUserIdAsync(countryId, string.Empty);
        }

        public async Task<CountryResponse> GetFullByIdAndUserIdAsync(string countryId, string userId)
        {
            try
            {
                CountryCaching countryCaching = await _countryCachingService.GetAsync(countryId);
                if (countryCaching is null) return null;

                CountryResponse country = countryCaching.Country;
                List<VisitResponse> visits = null;
                if(!string.IsNullOrEmpty(userId))
                    visits = _mapper.Map<List<VisitResponse>>(await _visitRepository.GetAsync(x => x.UserId.Equals(userId)));

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
    }
}
