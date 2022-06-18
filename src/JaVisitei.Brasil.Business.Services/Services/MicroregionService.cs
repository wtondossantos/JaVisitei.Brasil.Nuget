using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class MicroregionService : ReadOnlyService<Microregion>, IMicroregionService
    {
        private readonly IMicroregionRepository _microregionRepository;
        private readonly IMapper _mapper;

        public MicroregionService(IMicroregionRepository microregionRepository, 
            IMapper mapper) : base(microregionRepository, mapper)
        {
            _microregionRepository = microregionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<M>> GetByStateAsync<M>(string stateId)
        {
            var items = await _microregionRepository.GetByStateAsync(stateId);
            return items is null || !items.Any() ? default : _mapper.Map<IEnumerable<M>>(items);
        }
    }
}
