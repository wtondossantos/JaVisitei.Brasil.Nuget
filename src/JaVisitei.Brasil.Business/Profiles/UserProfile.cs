using AutoMapper;
using JaVisitei.Brasil.Business.Enums;
using JaVisitei.Brasil.Business.ViewModels.Request.Email;
using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.ViewModels.Response.User;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Helper.Others;
using System;

namespace JaVisitei.Brasil.Business.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddUserRequest, AddFullUserRequest>()
                .AfterMap((src, dest) => dest.UserRoleId = 3);

            CreateMap<AddFullUserRequest, User>()
                .BeforeMap((src, dest) => {
                    src.Password = Encrypt.Sha256encrypt(src.Password);
                    src.Username = src.Username.ToLower();
                    src.Email = src.Email.ToLower();
                    src.ReEmail = src.ReEmail.ToLower();
                })
                .AfterMap((src, dest) => {
                    dest.RegistryDate = DateTime.Now;
                    dest.SecurityStamp = Guid.NewGuid().ToString();
                });
            CreateMap<AddUserRequest, User>()
                .BeforeMap((src, dest) => {
                    src.Password = Encrypt.Sha256encrypt(src.Password);
                    src.Username = src.Username.ToLower();
                    src.Email = src.Email.ToLower();
                    src.ReEmail = src.ReEmail.ToLower();
                })
                .AfterMap((src, dest) => {
                    dest.RegistryDate = DateTime.Now;
                    dest.SecurityStamp = Guid.NewGuid().ToString();
                });

            CreateMap<User, UserResponse>()
                .AfterMap((src, dest) => {
                    dest.Password = String.Empty;
                });

            CreateMap<EditUserRequest, EditFullUserRequest>();

            CreateMap<EditFullUserRequest, User>()
                .BeforeMap((src, dest) => {
                    src.Username = src.Username.ToLower();
                    src.Email = src.Email.ToLower();
                    src.ReEmail = src.ReEmail.ToLower();
                    src.UserRoleId = src.UserRoleId == 0 ? (int)UserRoleEnum.Basic : src.UserRoleId;
                })
                .AfterMap((src, dest) => {
                    dest.SecurityStamp = Guid.NewGuid().ToString();
                });
        }
    }
}
