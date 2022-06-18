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
    public class ArchipelagoService : ReadOnlyService<Archipelago>, IArchipelagoService
    {
        private readonly IArchipelagoRepository _archipelagoRepository;
        private readonly IMapper _mapper;

        public ArchipelagoService(IArchipelagoRepository archipelagoRepository,
            IMapper mapper) : base(archipelagoRepository, mapper)
        {
            _archipelagoRepository = archipelagoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<M>> GetByStateAsync<M>(string stateId)
        {
            var items = await _archipelagoRepository.GetByStateAsync(stateId);
            return items is null || !items.Any() ? default : _mapper.Map<IEnumerable<M>>(items);
        }
    }
}
