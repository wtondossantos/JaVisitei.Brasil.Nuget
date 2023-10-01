using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Business.Enums;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class VisitService : Service<Visit>, IVisitService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IMunicipalityService _municipalityService;
        private readonly IIslandService _islandService;
        private readonly IStateService _stateService;
        private readonly ICountryService _countryService;
        private readonly IUserService _userService;
        private readonly VisitValidator _visitValidator;
        private readonly IMapper _mapper;

        public VisitService(IVisitRepository visitRepository,
            IMunicipalityService municipalityService,
            IIslandService islService,
            IUserService userService,
            IStateService stateService,
            ICountryService countryService,
            VisitValidator visitValidator,
            IMapper mapper) : base(visitRepository, mapper)
        {
            _visitRepository = visitRepository;
            _municipalityService = municipalityService;
            _islandService = islService;
            _userService = userService;
            _stateService = stateService;
            _countryService = countryService;
            _visitValidator = visitValidator;   
            _mapper = mapper;
        }

        public async Task<M> GetByIdAsync<M>(VisitKeyRequest request)
        {
            return await GetFirstOrDefaultAsync<M>(x => x.UserId.Equals(request.UserId) &&
                            x.RegionId.Equals(request.RegionId) &&
                            x.RegionTypeId.Equals(request.RegionTypeId));
        }

        public async Task<IEnumerable<M>> GetByUserIdAsync<M>(string userId)
        {
            return await GetAsync<M>(x => x.UserId.Equals(userId));
        }

        public async Task<VisitValidator> InsertAsync(InsertVisitRequest request)
        {
            try
            {
                _visitValidator.ValidatesVisitCreation(request);

                if (!_visitValidator.IsValid)
                    return _visitValidator;

                switch ((RegionTypeEnum)request.RegionTypeId)
                {
                    case RegionTypeEnum.Municipality:
                        if (!await _municipalityService.AnyAsync(x => x.Id.Equals(request.RegionId)))
                            _visitValidator.Errors.Add("Município não encontrado.");

                        break;
                    case RegionTypeEnum.Island:
                        if (!await _islandService.AnyAsync(x => x.Id.Equals(request.RegionId)))
                            _visitValidator.Errors.Add("Ilha não encontrada.");

                        break;
                    case RegionTypeEnum.State:
                        if (!await _stateService.AnyAsync(x => x.Id.Equals(request.RegionId)))
                            _visitValidator.Errors.Add("Divisão do pais não encontrado.");

                        break;
                    case RegionTypeEnum.Country:
                        if (!await _countryService.AnyAsync(x => x.Id.Equals(request.RegionId)))
                            _visitValidator.Errors.Add("Pais não encontrado.");

                        break;
                    default:
                        _visitValidator.Errors.Add("Tipo de região inválida.");
                        break;
                }

                if (!_visitValidator.IsValid)
                    return _visitValidator;

                var visit = _mapper.Map<Visit>(request);

                if (await _visitRepository.AnyAsync(visit))
                {
                    _visitValidator.Errors.Add("Visita já registrada.");
                    return _visitValidator;
                }

                if (!await _userService.AnyAsync(x => x.Id.Equals(visit.UserId)))
                { 
                    _visitValidator.Errors.Add("Usuário não encontrado.");
                    return _visitValidator;
                }

                if (await _visitRepository.InsertAsync(visit))
                {

                    _visitValidator.Data = _mapper.Map<VisitResponse>(visit);
                    _visitValidator.Message = "Visita registrada com sucesso.";
                }
                else
                    _visitValidator.Errors.Add($"Erro ao registrar visita.");
            }
            catch (Exception ex)
            {
                _visitValidator.Errors.Add($"Erro ao registrar visita: {ex.Message}");
            }

            return _visitValidator;
        }

        public async Task<VisitValidator> UpdateAsync(UpdateVisitRequest request)
        {
            try
            {
                _visitValidator.ValidatesVisitEdition(request);

                if (!_visitValidator.IsValid)
                    return _visitValidator;

                switch ((RegionTypeEnum)request.RegionTypeId)
                {
                    case RegionTypeEnum.Municipality:
                        if (!await _municipalityService.AnyAsync(x => x.Id.Equals(request.RegionId)))
                            _visitValidator.Errors.Add("Município não encontrado.");

                        break;
                    case RegionTypeEnum.Island:
                        if (!await _islandService.AnyAsync(x => x.Id.Equals(request.RegionId)))
                            _visitValidator.Errors.Add("Ilha não encontrada.");

                        break;
                    case RegionTypeEnum.State:
                        if (!await _stateService.AnyAsync(x => x.Id.Equals(request.RegionId)))
                            _visitValidator.Errors.Add("Divisão do pais não encontrado.");

                        break;
                    case RegionTypeEnum.Country:
                        if (!await _countryService.AnyAsync(x => x.Id.Equals(request.RegionId)))
                            _visitValidator.Errors.Add("Pais não encontrado.");

                        break;
                    default:
                        _visitValidator.Errors.Add("Tipo de região inválida.");
                        break;
                }

                if (!_visitValidator.IsValid)
                    return _visitValidator;

                var visit = _mapper.Map<Visit>(request);

                if (!await _userService.AnyAsync(x => x.Id.Equals(visit.UserId)))
                {
                    _visitValidator.Errors.Add("Usuário não encontrado.");
                    return _visitValidator;
                }

                if (await _visitRepository.AnyAsync(visit) && await _visitRepository.UpdateAsync(visit))
                {
                    _visitValidator.Data = _mapper.Map<VisitResponse>(visit);
                    _visitValidator.Message = "Visita atualizada com sucesso.";
                }
                else
                    _visitValidator.Errors.Add($"Erro ao atualizar visita.");
            }
            catch (Exception ex)
            {
                _visitValidator.Errors.Add($"Erro ao atualizar visita: {ex.Message}");
            }

            return _visitValidator;
        }

        public async Task<VisitValidator> DeleteAsync(VisitKeyRequest request)
        {
            try
            {
                _visitValidator.ValidatesVisitDelete(request);

                if (!_visitValidator.IsValid)
                    return _visitValidator;

                var visit = await GetByIdAsync<Visit>(request);

                if (visit is null)
                {
                    _visitValidator.Data = new VisitResponse
                    {
                        UserId = request.UserId
                    };
                    _visitValidator.Message = "Visita já removida.";
                    return _visitValidator;
                }
                
                if (await _visitRepository.DeleteAsync(visit))
                {
                    _visitValidator.Data = new VisitResponse
                    {
                        UserId = visit.UserId
                    };
                    _visitValidator.Message = "Visita removida com sucesso.";
                }
                else
                    _visitValidator.Errors.Add($"Erro ao remover visita.");

            }
            catch (Exception ex)
            {
                _visitValidator.Errors.Add($"Erro ao remover visita: {ex.Message}");
            }

            return _visitValidator;
        }
    }
}
