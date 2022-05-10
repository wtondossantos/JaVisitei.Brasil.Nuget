using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Business.Enums;
using JaVisitei.Brasil.Data.Entities;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System;
using JaVisitei.Brasil.Business.Validation.Validators;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class VisitService : BaseService<Visit>, IVisitService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IMunicipalityRepository _municipioRepository;
        private readonly IUserRepository _userRepository;
        private readonly VisitValidator _visitValidator;
        private readonly IMapper _mapper;

        public VisitService(IVisitRepository visitRepository, 
            IMunicipalityRepository municipioRepository,
            IUserRepository userRepository,
            VisitValidator visitValidator,
            IMapper mapper) : base(visitRepository)
        {
            _visitRepository = visitRepository;
            _municipioRepository = municipioRepository;
            _userRepository = userRepository;
            _visitValidator = visitValidator;   
            _mapper = mapper;
        }

        public async Task<VisitValidator> AddAsync(AddVisitRequest request)
        {
            try
            {
                _visitValidator.ValidatesVisitCreation(request);

                if(!_visitValidator.IsValid)
                    return _visitValidator;

                switch ((RegionTypeEnum)request.RegionTypeId)
                {
                    case RegionTypeEnum.Municipality:
                        var municipality = await _municipioRepository.GetAsync(x => x.Id.Equals(request.RegionId));
                        if (municipality.ToList().Count == 0)
                        {
                            _visitValidator.Errors.Add("Município não encontrado.");
                            return _visitValidator;
                        }
                        break;
                    default:
                        _visitValidator.Errors.Add("Tipo de região inválida. Código do tipo de região disponível: (6).");
                        return _visitValidator;
                }

                var duplicateVisit = await _visitRepository.GetAsync(x => x.UserId.Equals(request.UserId) && 
                                                                x.RegionTypeId.Equals(request.RegionTypeId) && 
                                                                x.RegionId.Equals(request.RegionId));
                if (duplicateVisit.ToList().Count > 0)
                {
                    _visitValidator.Errors.Add("Visita já registrada.");
                    return _visitValidator; 
                }

                var user = await _userRepository.GetAsync(x => x.Id.Equals(request.UserId));
                if (user.ToList().Count == 0)
                    _visitValidator.Errors.Add("Usuário não encontrado.");

                else
                {
                    var visit = _mapper.Map<Visit>(request);
                    var visitResult = await _visitRepository.AddAsync(visit);

                    if (visitResult)
                    {
                        var response = _mapper.Map<VisitResponse>(visit);

                        _visitValidator.Data = response;
                        _visitValidator.Message = "Visita registrada com sucesso.";
                    }
                    else
                        _visitValidator.Errors.Add($"Erro ao registrar visita.");
                }
            }
            catch (Exception ex)
            {
                _visitValidator.Errors.Add($"Erro ao registrar visita: {ex.Message}");
            }

            return _visitValidator;
        }
    }
}
