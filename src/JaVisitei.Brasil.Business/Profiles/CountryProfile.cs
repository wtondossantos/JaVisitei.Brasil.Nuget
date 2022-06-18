using JaVisitei.Brasil.Business.ViewModels.Response.Country;
using JaVisitei.Brasil.Data.Entities;
using AutoMapper;

namespace JaVisitei.Brasil.Business.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryResponse>();
        }
    }
}
