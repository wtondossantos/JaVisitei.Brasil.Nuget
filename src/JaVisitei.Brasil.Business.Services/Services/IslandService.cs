using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class IslandService : BaseService<Island>, IIslandService
    {
        private readonly IIslandRepository _islandRepository;

        public IslandService(IIslandRepository islandRepository) : base(islandRepository)
        {
            _islandRepository = islandRepository;
        }

        public async Task<IEnumerable<Island>> GetByStateAsync(string id)
        {
            return await _islandRepository.GetByStateAsync(id);
        }

        public async Task<IEnumerable<Island>> GetByMacroregionAsync(string id)
        {
            return await _islandRepository.GetByMacroregionAsync(id);
        }
    }
}
