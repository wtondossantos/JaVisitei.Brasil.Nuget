using JaVisitei.Brasil.Business.ViewModels.Response.State;
using JaVisitei.Brasil.Data.Entities;
using AutoMapper;

namespace JaVisitei.Brasil.Business.Profiles
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<State, StateResponse>();
        }
    }
}
