using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
using JaVisitei.Brasil.Business.ViewModels.Response.Country;
using JaVisitei.Brasil.Business.ViewModels.Response.Island;
using JaVisitei.Brasil.Business.ViewModels.Response.Macroregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using JaVisitei.Brasil.Business.ViewModels.Response.Municipality;
using JaVisitei.Brasil.Business.ViewModels.Response.State;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.CacheModels
{
    public class CountryCaching
    {
        public CountryResponse Country { get; set; }
        public List<StateResponse> States { get; set; }
        public List<MacroregionResponse> Macroregions { get; set; }
        public List<MicroregionResponse> Microregions { get; set; }
        public List<ArchipelagoResponse> Archipelagos { get; set; }
        public List<MunicipalityResponse> Municipalities { get; set; }
        public List<IslandResponse> Islands { get; set; }
    }
}
