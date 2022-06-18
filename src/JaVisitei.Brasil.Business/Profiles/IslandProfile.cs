using JaVisitei.Brasil.Business.ViewModels.Response.Island;
using JaVisitei.Brasil.Data.Entities;
using AutoMapper;

namespace JaVisitei.Brasil.Business.Profiles
{
    public class IslandProfile : Profile
    {
        public IslandProfile()
        {
            CreateMap<Island, IslandResponse>();
        }
    }
}
