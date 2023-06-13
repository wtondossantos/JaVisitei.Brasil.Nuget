using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.ViewModels.Response.User;
using JaVisitei.Brasil.Business.Enums;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Helper.Others;
using AutoMapper;
using System;

namespace JaVisitei.Brasil.Business.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<InsertUserRequest, InsertFullUserRequest>()
                .AfterMap((src, dest) => dest.UserRoleId = 3);

            CreateMap<InsertFullUserRequest, User>()
                .BeforeMap((src, dest) => {
                    src.Password = Encrypt.Sha256encrypt(src.Password);
                    src.Username = src.Username.ToLower();
                    src.Email = src.Email.ToLower();
                    src.ReEmail = src.ReEmail.ToLower();
                })
                .AfterMap((src, dest) => {
                    dest.Id = Guid.NewGuid().ToString();
                    dest.RegistryDate = DateTime.Now;
                    dest.RefreshTokenDate = DateTime.Now;
                    dest.SecurityStamp = Guid.NewGuid().ToString();
                    dest.RefreshToken = Guid.NewGuid().ToString();
                });

            CreateMap<InsertUserRequest, User>()
                .BeforeMap((src, dest) => {
                    src.Password = Encrypt.Sha256encrypt(src.Password);
                    src.Username = src.Username.ToLower();
                    src.Email = src.Email.ToLower();
                    src.ReEmail = src.ReEmail.ToLower();
                })
                .AfterMap((src, dest) => {
                    dest.Id = Guid.NewGuid().ToString();
                    dest.RegistryDate = DateTime.Now;
                    dest.RefreshTokenDate = DateTime.Now;
                    dest.SecurityStamp = Guid.NewGuid().ToString();
                    dest.RefreshToken = Guid.NewGuid().ToString();
                });

            CreateMap<User, UserResponse>()
                .AfterMap((src, dest) => {
                    dest.Password = string.Empty;
                });

            CreateMap<UpdateUserRequest, UpdateFullUserRequest>();

            CreateMap<UpdateFullUserRequest, User>()
                .BeforeMap((src, dest) => {
                    src.Username = src.Username.ToLower();
                    src.Email = src.Email.ToLower();
                    src.ReEmail = src.ReEmail.ToLower();
                    src.UserRoleId = src.UserRoleId.Equals(0) ? (int)UserRoleEnum.Basic : src.UserRoleId;
                })
                .AfterMap((src, dest) => {
                    dest.SecurityStamp = Guid.NewGuid().ToString();
                    dest.Password ??= String.Empty;
                });
        }
    }
}
