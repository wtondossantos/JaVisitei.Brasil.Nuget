using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Caching.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JaVisitei.Brasil.Business.ViewModels.Response.State;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class StateService : ReadOnlyService<State>, IStateService
    {
        private readonly IStateCachingService _stateCachingService;

        public StateService(IStateRepository stateRepository, IMapper mapper,
            IStateCachingService stateCachingService) : base(stateRepository, mapper) {
            _stateCachingService = stateCachingService;
        }

        public async Task<List<StateSearchResponse>> GetNamesAsync(short mapTypeId)
        {
            return await _stateCachingService.GetNamesAsync(mapTypeId, null);
        }
    }
}
