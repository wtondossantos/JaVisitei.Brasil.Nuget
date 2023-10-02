using JaVisitei.Brasil.Business.ViewModels.Response.Country;
using JaVisitei.Brasil.Data.Entities;
using AutoMapper;
using JaVisitei.Brasil.Business.ViewModels.Response.Base;

namespace JaVisitei.Brasil.Business.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryResponse>();
            CreateMap<Country, BasicResponse>();
        }
    }
}
