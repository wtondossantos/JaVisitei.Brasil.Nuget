using JaVisitei.Brasil.Business.ViewModels.Response.Archipelago;
using JaVisitei.Brasil.Data.Entities;
using AutoMapper;

namespace JaVisitei.Brasil.Business.Profiles
{
    public class ArchipelagoProfile : Profile
    {
        public ArchipelagoProfile()
        {
            CreateMap<Archipelago, ArchipelagoResponse>();
        }
    }
}
