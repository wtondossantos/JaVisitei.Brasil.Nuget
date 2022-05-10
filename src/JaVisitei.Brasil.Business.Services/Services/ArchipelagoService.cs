using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class ArchipelagoService : BaseService<Archipelago>, IArchipelagoService
    {
        private readonly IArchipelagoRepository _archipelagoRepository;

        public ArchipelagoService(IArchipelagoRepository archipelagoRepository) : base(archipelagoRepository)
        {
            _archipelagoRepository = archipelagoRepository;
        }

        public async Task<IEnumerable<Archipelago>> GetByStateAsync(string id)
        {
            return await _archipelagoRepository.GetByStateAsync(id);
        }

    }
}
