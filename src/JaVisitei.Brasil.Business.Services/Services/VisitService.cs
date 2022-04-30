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
            var validacao = new VisitValidation();
            var retorno = new AddVisitResponse();
            retorno.Validation = new ValidationResponse();
            retorno.Validation.Message = new List<string>();

            try
            {
                retorno.Validation.Message = validacao.ValidaRegistroVisita(request);
                if (retorno.Validation.Message != null && retorno.Validation.Message.Count > 0)
                    return retorno;

                switch ((RegionTypeEnum)request.RegionTypeId)
                {
                    case RegionTypeEnum.Municipality:
                        var municipio = await _municipioRepository.GetAsync(x => x.Id == request.RegionId);
                        if (municipio.ToList().Count == 0)
                        {
                            retorno.Validation.Message.Add("Município não encontrado.");
                            return retorno;
                        }
                        break;
                    default:
                        retorno.Validation.Message.Add("Tipo de região inválida. Código do tipo de região disponível: (6).");
                        return retorno;
                }

                var visitaDuplicada = await _visitRepository.GetAsync(x => x.UserId == request.UserId && x.RegionTypeId == request.RegionTypeId && x.RegionId == request.RegionId);
                if (visitaDuplicada.ToList().Count > 0)
                {
                    retorno.Validation.Message.Add("Visita já registrada.");
                    return retorno; 
                }

                var usuario = await _userRepository.GetAsync(x => x.Id == request.UserId);
                if (usuario.ToList().Count <= 0)
                    retorno.Validation.Message.Add("Usuário não encontrado.");

                else
                {

                    var visita = _mapper.Map<Visit>(request);
                    var status = await _visitRepository.AddAsync(visita);

                    if (status == 1)
                    {
                        retorno = _mapper.Map<AddVisitResponse>(visita);

                        retorno.Validation = new ValidationResponse();
                        retorno.Validation.Successfully = true;
                        retorno.Validation.Message = new List<string>();
                        retorno.Validation.Message.Add("Visita registrada com sucesso.");
                    }
                    else
                        retorno.Validation.Message.Add($"Erro ao registrar visita.");

                    retorno.Validation.Code = status;
                }
            }
            catch (Exception ex)
            {
                retorno.Validation.Message.Add($"Erro ao registrar visita: {ex.Message}");
                retorno.Validation.Code = -1;
            }

            return retorno;
        }
    }
}
