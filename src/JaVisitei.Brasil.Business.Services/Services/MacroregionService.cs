using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class MacroregionService : BaseService<Macroregion>, IMacroregionService
    {
        public MacroregionService(IMacroregionRepository macroregionRepository) : base(macroregionRepository) { }
    }
}
