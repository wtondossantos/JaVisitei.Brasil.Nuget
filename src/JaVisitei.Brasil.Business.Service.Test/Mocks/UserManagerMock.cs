﻿using JaVisitei.Brasil.Business.Constants;
using JaVisitei.Brasil.Business.ViewModels.Request.UserManager;
using JaVisitei.Brasil.Data.Entities;
using System;

namespace JaVisitei.Brasil.Business.Service.Test.Mocks
{
    public static class UserManagerMock
    {
        public static UserManager ReturnUserManagerNotConfirmedMock()
        {
            return new UserManager
            {
                Id = 1,
                UserId = Guid.NewGuid().ToString(),
                EmailId = 1,
                ManagerCode = "ABC123AB99",
                ConfirmedChange = false,
                ExpirationDate = DateTime.Now.AddMinutes(Constant.CONFIRMATION_CODE_EXPIRATION_TIME_EMAIL),
                Email = new Email(),
                User = new User()
            };
        }

        public static UserManager ReturnUserManagerNotConfirmedManagerCodeMock()
        {
            return new UserManager
            {
                Id = 1,
                UserId = Guid.NewGuid().ToString(),
                EmailId = 1,
                ManagerCode = "ABC123AB",
                ConfirmedChange = false,
                ExpirationDate = DateTime.Now.AddMinutes(Constant.CONFIRMATION_CODE_EXPIRATION_TIME_EMAIL),
                Email = new Email(),
                User = new User()
            };
        }
        public static UserManager ReturnUserManagerMock()
        {
            return new UserManager
            {
                Id = 1,
                UserId = Guid.NewGuid().ToString(),
                EmailId = 1,
                ManagerCode = "ABC123AB99",
                ConfirmedChange = true,
                ExpirationDate = DateTime.Now.AddMinutes(Constant.CONFIRMATION_CODE_EXPIRATION_TIME_EMAIL),
                Email = new Email(),
                User = new User()
            };
        }

        public static UserManager ReturnUserManagerMapperMock()
        {
            return new UserManager
            {
                UserId = Guid.NewGuid().ToString()
            };
        }

        public static UserManager ReturnUserManagerExpirationDateMock()
        {
            return new UserManager
            {
                Id = 1,
                UserId = Guid.NewGuid().ToString(),
                EmailId = 1,
                ManagerCode = "ABC123AB00",
                ConfirmedChange = true,
                ExpirationDate = DateTime.Now.AddMinutes(Constant.CONFIRMATION_CODE_EXPIRATION_TIME_EMAIL).AddSeconds(-1),
                Email = new Email(),
                User = new User()
            };
        }

        public static InsertEmailConfirmationUserManagerRequest InsertEmailConfirmationUserManagerRequestMock()
        {
            return new InsertEmailConfirmationUserManagerRequest
            {
                UserId = Guid.NewGuid().ToString()
            };
        }

        public static InsertPasswordResetUserManagerRequest InsertPasswordResetUserManagerRequestMock()
        {
            return new InsertPasswordResetUserManagerRequest
            {
                UserId = Guid.NewGuid().ToString()
            };
        }
    }
}
