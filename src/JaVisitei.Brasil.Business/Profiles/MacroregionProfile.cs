using JaVisitei.Brasil.Business.ViewModels.Response.Macroregion;
using JaVisitei.Brasil.Data.Entities;
using AutoMapper;

namespace JaVisitei.Brasil.Business.Profiles
{
    public class MacroregionProfile : Profile
    {
        public MacroregionProfile()
        {
            CreateMap<Macroregion, MacroregionResponse>();
        }
    }
}
