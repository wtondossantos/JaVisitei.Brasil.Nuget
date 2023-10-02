using JaVisitei.Brasil.Business.ViewModels.Response.Microregion;
using JaVisitei.Brasil.Data.Entities;
using AutoMapper;

namespace JaVisitei.Brasil.Business.Profiles
{
    public class MicroregionProfile : Profile
    {
        public MicroregionProfile()
        {
            CreateMap<Microregion, MicroregionResponse>();
        }
    }
}
