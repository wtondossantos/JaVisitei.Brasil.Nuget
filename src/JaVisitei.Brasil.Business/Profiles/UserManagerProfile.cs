using AutoMapper;
using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.Enums;
using JaVisitei.Brasil.Business.ViewModels.Request.UserManager;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Security;
using System;

namespace JaVisitei.Brasil.Business.Profiles
{ 
    public class UserManagerProfile : Profile
    {
        public UserManagerProfile()
        {
            CreateMap<InsertEmailConfirmationUserManagerRequest, UserManager>()
                .AfterMap((src, dest) =>
                {
                    dest.EmailId = (int)EmailEnum.ConfirmationEmail;
                    dest.ManagerCode = TokenString.GenerateEmailConfirmationToken();
                    dest.ConfirmedChange = false;
                    dest.ExpirationDate = DateTime.Now.AddMinutes(Constant.CONFIRMATION_CODE_EXPIRATION_TIME_EMAIL);
                });

            CreateMap<InsertPasswordResetUserManagerRequest, UserManager>()
                .AfterMap((src, dest) =>
                {
                    dest.EmailId = (int)EmailEnum.ForgotPassword;
                    dest.ManagerCode = TokenString.GeneratePasswordResetToken();
                    dest.ConfirmedChange = false;
                    dest.ExpirationDate = DateTime.Now.AddMinutes(Constant.CONFIRMATION_CODE_EXPIRATION_TIME_PASSWORD);
                });

        }
    }
}
