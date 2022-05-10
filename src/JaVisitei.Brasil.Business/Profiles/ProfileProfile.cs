using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Helper.Others;
using AutoMapper;
using System;

namespace JaVisitei.Brasil.Business.Profiles
{
    public class ProfileProfile : Profile
    {
        public ProfileProfile()
        {

            CreateMap<LoginRequest, User>()
                .BeforeMap((src, dest) => {
                    src.Password = Encrypt.Sha256encrypt(src.Password);
                    src.Email = src.Email.ToLower();
                    })
                    .AfterMap((src, dest) => {
                        dest.SecurityStamp = Guid.NewGuid().ToString();
                    });

            CreateMap<ResetPasswordRequest, User>()
                .BeforeMap((src, dest) => {
                    src.Password = Encrypt.Sha256encrypt(src.Password);
                    src.Email = src.Email.ToLower();
                })
                .AfterMap((src, dest) => {
                    dest.SecurityStamp = Guid.NewGuid().ToString();
                });
        }
    }
}
