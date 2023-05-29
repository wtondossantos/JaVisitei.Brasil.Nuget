using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using JaVisitei.Brasil.Business.ViewModels.Response.Country;
using JaVisitei.Brasil.Business.ViewModels.Response.State;
using JaVisitei.Brasil.Business.ViewModels.Response.Macroregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using JaVisitei.Brasil.Business.ViewModels.Response.Island;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using System;
using JaVisitei.Brasil.Helper.Formatting;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class CountryService : ReadOnlyService<Country>, ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IMacroregionRepository _macroregionRepository;
        private readonly IMicroregionRepository _microregionRepository;
        private readonly IArchipelagoRepository _archipelagoRepository;
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly IIslandRepository _islandRepository;
        private readonly IVisitRepository _visitRepository;
        private readonly IMapper _mapper;

        public CountryService(
                ICountryRepository countryRepository,
                IStateRepository stateRepository,
                IMacroregionRepository macroregionRepository,
                IMicroregionRepository microregionRepository,
                IArchipelagoRepository archipelagoRepository,
                IMunicipalityRepository municipalityRepository,
                IIslandRepository islandRepository,
                IVisitRepository visitRepository,
                IMapper mapper) : base(countryRepository, mapper) {
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _archipelagoRepository = archipelagoRepository;
            _macroregionRepository = macroregionRepository; 
            _microregionRepository = microregionRepository;
            _municipalityRepository = municipalityRepository;
            _islandRepository = islandRepository;
            _visitRepository = visitRepository;
            _mapper = mapper;
        }

        public async Task<CountryResponse> GetFullByIdAsync(string countryId) 
        {
            return await GetFullByIdAndUserIdAsync(countryId, string.Empty);
        }

        public async Task<CountryResponse> GetFullByIdAndUserIdAsync(string countryId, string userId)
        {
            try
            {
                var country = _mapper.Map<CountryResponse>(await _countryRepository.GetByIdAsync(countryId));
                var states = _mapper.Map<List<StateResponse>>(await _stateRepository.GetAsync(x => x.CountryId.Equals(countryId)));
                var macroregions = _mapper.Map<List<MacroregionResponse>>(await _macroregionRepository.GetAsync());
                var microregions = _mapper.Map<List<MicroregionResponse>>(await _microregionRepository.GetAsync());
                var archipelagos = _mapper.Map<List<ArchipelagoResponse>>(await _archipelagoRepository.GetAsync());
                var municipalities = _mapper.Map<List<MunicipalityResponse>>(await _municipalityRepository.GetAsync());
                var islands = _mapper.Map<List<IslandResponse>>(await _islandRepository.GetAsync());

                List<VisitResponse> visits = null;
                if(!string.IsNullOrEmpty(userId))
                    visits = _mapper.Map<List<VisitResponse>>(await _visitRepository.GetAsync(x => x.UserId.Equals(Convert.ToInt32(userId))));

                foreach (var state in states)
                {
                    var listMacroregion = macroregions.Where(x => x.StateId.Equals(state.Id));
                    foreach (var macro in listMacroregion)
                    {
                        var listMicroregion = microregions.Where(x => x.MacroregionId.Equals(macro.Id));
                        foreach (var micro in listMicroregion)
                        {
                            var listMunicipality = municipalities.Where(x => x.MicroregionId.Equals(micro.Id));
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

                        var listArchipelago = archipelagos.Where(x => x.MacroregionId.Equals(macro.Id));
                        foreach (var archipelago in listArchipelago)
                        {
                            var listIsland = islands.Where(x => x.ArchipelagoId.Equals(archipelago.Id));
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
