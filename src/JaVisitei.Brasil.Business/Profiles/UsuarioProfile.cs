using AutoMapper;
using JaVisitei.Brasil.Business.ViewModels.Request;
using JaVisitei.Brasil.Data.Entities;

namespace JaVisitei.Brasil.Business.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<LoginRequest, Usuario>();
        }
    }
}
