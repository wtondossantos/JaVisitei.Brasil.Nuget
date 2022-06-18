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
        private readonly IUserService _userService;
        private readonly VisitValidator _visitValidator;
        private readonly IMapper _mapper;

        public VisitService(IVisitRepository visitRepository,
            IMunicipalityService municipalityService,
            IIslandService islService,
            IUserService userService,
            VisitValidator visitValidator,
            IMapper mapper) : base(visitRepository, mapper)
        {
            _visitRepository = visitRepository;
            _municipalityService = municipalityService;
            _islandService = islService;
            _userService = userService;
            _visitValidator = visitValidator;   
            _mapper = mapper;
        }

        public async Task<M> GetByIdAsync<M>(VisitKeyRequest request)
        {
            return await GetFirstOrDefaultAsync<M>(x => x.UserId.Equals(request.UserId) &&
                            x.RegionId.Equals(request.RegionId) &&
                            x.RegionTypeId.Equals(request.RegionTypeId));
        }

        public async Task<IEnumerable<M>> GetByUserIdAsync<M>(int userId)
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
                    default:
                        _visitValidator.Errors.Add("Tipo de região inválida. Código do tipo de região disponível: (6) para município e (7) para ilha.");
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
