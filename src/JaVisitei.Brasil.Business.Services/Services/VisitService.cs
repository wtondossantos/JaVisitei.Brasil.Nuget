using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using JaVisitei.Brasil.Business.ViewModels.Response;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Business.Validations;
using JaVisitei.Brasil.Business.Enums;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System;

namespace JaVisitei.Brasil.Business.Service
{
    public class VisitService : BaseService<Visit>, IVisitService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IMunicipalityRepository _municipioRepository;
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;

        public VisitService(IVisitRepository visitRepository, 
            IMunicipalityRepository municipioRepository,
            IUserRepository userRepository,
            IMapper mapper) : base(visitRepository)
        {
            _visitRepository = visitRepository;
            _municipioRepository = municipioRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AddVisitResponse> AddAsync(AddVisitRequest request)
        {
            var validation = new VisitValidation();
            var response = new AddVisitResponse();
            response.Validation = new ValidationResponse();
            response.Validation.Message = new List<string>();

            try
            {
                response.Validation.Message = validation.ValidatesVisitCreation(request);
                if (response.Validation.Message.Count > 0)
                    return response;

                switch ((RegionTypeEnum)request.RegionTypeId)
                {
                    case RegionTypeEnum.Municipality:
                        var municipality = await _municipioRepository.GetAsync(x => x.Id == request.RegionId);
                        if (municipality.ToList().Count == 0)
                        {
                            response.Validation.Message.Add("Município não encontrado.");
                            return response;
                        }
                        break;
                    default:
                        response.Validation.Message.Add("Tipo de região inválida. Código do tipo de região disponível: (6).");
                        return response;
                }

                var duplicateVisit = await _visitRepository.GetAsync(x => x.UserId == request.UserId && x.RegionTypeId == request.RegionTypeId && x.RegionId == request.RegionId);
                if (duplicateVisit.ToList().Count > 0)
                {
                    response.Validation.Message.Add("Visita já registrada.");
                    return response; 
                }

                var user = await _userRepository.GetAsync(x => x.Id == request.UserId);
                if (user.ToList().Count <= 0)
                    response.Validation.Message.Add("Usuário não encontrado.");

                else
                {

                    var visit = _mapper.Map<Visit>(request);
                    var status = await _visitRepository.AddAsync(visit);

                    if (status == 1)
                    {
                        response = _mapper.Map<AddVisitResponse>(visit);

                        response.Validation = new ValidationResponse();
                        response.Validation.Successfully = true;
                        response.Validation.Message = new List<string>();
                        response.Validation.Message.Add("Visita registrada com sucesso.");
                    }
                    else
                        response.Validation.Message.Add($"Erro ao registrar visita.");

                    response.Validation.Code = status;
                }
            }
            catch (Exception ex)
            {
                response.Validation.Message.Add($"Erro ao registrar visita: {ex.Message}");
                response.Validation.Code = -1;
            }

            return response;
        }
    }
}
