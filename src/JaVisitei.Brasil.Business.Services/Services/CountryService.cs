using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using JaVisitei.Brasil.Business.ViewModels.Response.Country;

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
        private readonly IMapper _mapper;

        public CountryService(
                ICountryRepository countryRepository,
                IStateRepository stateRepository,
                IMacroregionRepository macroregionRepository,
                IMicroregionRepository microregionRepository,
                IArchipelagoRepository archipelagoRepository,
                IMunicipalityRepository municipalityRepository,
                IIslandRepository islandRepository,
                IMapper mapper) : base(countryRepository, mapper) {
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _archipelagoRepository = archipelagoRepository;
            _macroregionRepository = macroregionRepository; 
            _microregionRepository = microregionRepository;
            _municipalityRepository = municipalityRepository;
            _islandRepository = islandRepository;
            _mapper = mapper;
        }

        public async Task<CountryResponse> GetFullByIdAsync(string countryId)
        {
            var country = await _countryRepository.GetByIdAsync(countryId);
            var states = await _stateRepository.GetAsync(x => x.CountryId.Equals(countryId));
            var macroregions = await _macroregionRepository.GetAsync();
            var microregions = await _microregionRepository.GetAsync();
            var archipelagos = await _archipelagoRepository.GetAsync();
            var municipalities = await _municipalityRepository.GetAsync();
            var islands = await _islandRepository.GetAsync();

            foreach (var state in states)
            {
                var listMacroregion = macroregions.Where(x => x.StateId.Equals(state.Id));
                foreach (var macro in listMacroregion)
                {
                    var listMicroregion = microregions.Where(x => x.MacroregionId.Equals(macro.Id));
                    foreach (var micro in listMicroregion)
                    {
                        micro.Municipalities = (List<Municipality>)municipalities.Where(x => x.MicroregionId.Equals(micro.Id));

                        macro.Microregions.Add(micro);
                    }

                    var listArchipelago = archipelagos.Where(x => x.MacroregionId.Equals(macro.Id));
                    foreach (var archipelago in listArchipelago)
                    {
                        archipelago.Islands = (List<Island>)islands.Where(x => x.ArchipelagoId.Equals(archipelago.Id));

                        macro.Archipelagos.Add(archipelago);
                    }
                    state.Macroregions.Add(macro);
                }

                country.States.Add(state);
            }

            return country is null ? default : _mapper.Map<CountryResponse>(country);
        }
    }
}
