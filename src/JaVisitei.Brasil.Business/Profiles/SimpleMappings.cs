using AutoMapper;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Helper.Others;
using System;

namespace JaVisitei.Brasil.Business.Profiles
{
    public class SimpleMappings : Profile
    {
        public SimpleMappings()
        {
            CreateMap<LoginRequest, User>()
                .BeforeMap((src, dest) => src.Password = Encrypt.Sha256encrypt(src.Password));

            CreateMap<AddUserRequest, User>()
                .BeforeMap((src, dest) => src.Password = Encrypt.Sha256encrypt(src.Password))
                .AfterMap((src, dest) => dest.RegistryDate = DateTime.Now);

            CreateMap<EditUserRequest, User>();

            CreateMap<AddVisitRequest, Visit>()
                .BeforeMap((src, dest) => src.Color ??= Utility.RandomHexString());

            CreateMap<Visit, AddVisitResponse>();
        }
    }
}
