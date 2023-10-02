using JaVisitei.Brasil.Business.ViewModels.Response.RegionType;
using JaVisitei.Brasil.Data.Entities;
using AutoMapper;

namespace JaVisitei.Brasil.Business.Profiles
{
    public class RegionTypeProfile : Profile
    {
        public RegionTypeProfile()
        {
            CreateMap<RegionType, RegionTypeResponse>();
        }
    }
}
