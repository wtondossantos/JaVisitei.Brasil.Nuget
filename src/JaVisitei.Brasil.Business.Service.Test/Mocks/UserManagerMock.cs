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
                UserId = 1,
                EmailId = 1,
                ManagerCode = "ABC123AB99",
                ConfirmedChange = false,
                ExpirationDate = DateTime.Now.AddMinutes(10),
                Email = new Email(),
                User = new User()
            };
        }

        public static UserManager ReturnUserManagerMock()
        {
            return new UserManager
            {
                Id = 1,
                UserId = 1,
                EmailId = 1,
                ManagerCode = "ABC123AB99",
                ConfirmedChange = true,
                ExpirationDate = DateTime.Now.AddMinutes(10),
                Email = new Email(),
                User = new User()
            };
        }

        public static UserManager ReturnUserManagerMapperMock()
        {
            return new UserManager
            {
                UserId = 1
            };
        }

        public static UserManager ReturnUserManagerExpirationDateMock()
        {
            return new UserManager
            {
                Id = 1,
                UserId = 1,
                EmailId = 1,
                ManagerCode = "ABC123AB00",
                ConfirmedChange = true,
                ExpirationDate = DateTime.Now.AddMinutes(-11),
                Email = new Email(),
                User = new User()
            };
        }

        public static InsertEmailConfirmationUserManagerRequest InsertEmailConfirmationUserManagerRequestMock()
        {
            return new InsertEmailConfirmationUserManagerRequest
            {
                UserId = 1
            };
        }

        public static InsertPasswordResetUserManagerRequest InsertPasswordResetUserManagerRequestMock()
        {
            return new InsertPasswordResetUserManagerRequest
            {
                UserId = 1
            };
        }
    }
}
