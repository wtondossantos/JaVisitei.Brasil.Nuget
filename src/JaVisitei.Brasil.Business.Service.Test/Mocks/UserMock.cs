using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.ViewModels.Response.User;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Helper.Others;
using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service.Test.Mocks
{
    public static class UserMock
    {
        public static InsertUserRequest CreateUserBasicRequestMock()
        {
            return new InsertUserRequest
            {
                Name = "Wellington",
                Username = "Silva",
                Surname = "wellington",
                Email = "teste@teste.com.zz",
                ReEmail = "teste@teste.com.zz",
                Password = "!Abc5678",
                RePassword = "!Abc5678"
            };
        }

        public static UserValidator CreatedUserBasicResponseMock()
        {
            return new UserValidator
            {
                Data = new UserResponse
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Wellington",
                    Username = "wellington",
                    Surname = "Silva",
                    Email = "teste@teste.com.zz",
                    Password = "12345678901234567890123456789",
                    UserRoleId = 3,
                    SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49"
                },
                Message = "sucesso"
            };
        }

        public static InsertFullUserRequest CreateUserBasicFullRequestMock()
        {
            return new InsertFullUserRequest
            {
                Name = "Wellington",
                Surname = "Silva",
                Username = "wellington",
                Email = "teste@teste.com.zz",
                ReEmail = "teste@teste.com.zz",
                Password = "!Abc5678",
                RePassword = "!Abc5678",
                UserRoleId = 3
            };
        }
        public static InsertFullUserRequest CreateUserInvalidRequestMock()
        {
            return new InsertFullUserRequest
            {
                Name = "",
                Surname = "Silva",
                Username = "wellington s",
                Email = "testetestecomzz",
                ReEmail = "testetestecom.zz",
                Password = "12345",
                RePassword = "12345",
                UserRoleId = 0
            };
        }

        public static UserValidator CreatedUserAdminResponseMock()
        {
            return new UserValidator
            {
                Data = new UserResponse
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Wellington",
                    Username = "wellington",
                    Surname = "Silva",
                    Email = "teste@teste.com.zz",
                    Password = "12345678901234567890123456789",
                    UserRoleId = 1,
                    SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49"
                },
                Message = "sucesso"
            };
        }

        public static UpdateUserRequest UpdateUserRequestMock()
        {
            return new UpdateUserRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = "David",
                Surname = "Coffee",
                Username = "wellingtonedit",
                Email = "teste1@teste.com",
                ReEmail = "teste1@teste.com.zz",
                OldPassword = "!Old56789",
                Password = "!Abc56789",
                RePassword = "!Abc56789",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49"
            };
        }

        public static UpdateFullUserRequest UpdateFullUserRequestMock()
        {
            return new UpdateFullUserRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = "John",
                Surname = "Bob",
                Username = "johnbob",
                Email = "teste1@teste.com.zz",
                ReEmail = "teste1@teste.com.zz",
                OldPassword = "!Old56789",
                Password = "!Abc56789",
                RePassword = "!Abc56789",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                UserRoleId = 2
            };
        }

        public static UpdateFullUserRequest UpdateFullUserPasswordNullRequestMock()
        {
            return new UpdateFullUserRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = "David",
                Surname = "Coffee",
                Username = "wellingtonedit",
                Email = "teste1@teste.com.zz",
                ReEmail = "teste1@teste.com.zz",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                UserRoleId = 2
            };
        }

        public static UpdateFullUserRequest UpdateFullUserInvalidRequestMock()
        {
            return new UpdateFullUserRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = "",
                Surname = "Coffee",
                Username = "wellingtonedit f",
                Email = "teste1testecomzz",
                ReEmail = "teste1testecozz",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                OldPassword = "1",
                Password = "bc56789",
                RePassword = "Abc56789",
                UserRoleId = 0
            };
        }

        public static UserValidator CreatedUserContributorResponseMock()
        {
            return new UserValidator
            {
                Data = new UserResponse
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "David",
                    Surname = "Coffee",
                    Username = "wellingtonedit",
                    Email = "teste1@teste.com.zz",
                    Password = "!Abc56789",
                    SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                    RegistryDate = DateTime.Now,
                    Actived = true,
                    UserRoleId = 2
                },
                Message = "sucesso"
            };
        }

        public static List<UserResponse> GetListUserTest()
        {
            return new List<UserResponse>
            {
                UserActivedBasicMock(),
                UserContributorInactiveMock(),
                UserActivedContributorMock()
            };
        }

        public static UserResponse UserActivedBasicMock()
        {
            return new UserResponse
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Wellington",
                Surname = "Silva",
                Username = "wellington",
                Email = "teste@teste.com.zz",
                RegistryDate = DateTime.Now,
                Actived = true,
                UserRoleId = 3
            };
        }

        public static UserResponse UserContributorInactiveMock()
        {
            return new UserResponse
            {
                Id = Guid.NewGuid().ToString(),
                Name = "John",
                Surname = "Bob",
                Username = "johnbob",
                Email = "teste@teste.com.zz",
                RegistryDate = DateTime.Now,
                Actived = false,
                UserRoleId = 2
            };
        }

        public static User UserContributorMock()
        { 
            return new User
            {
                Id = string.Empty,
                Name = "John",
                Surname = "Bob",
                Username = "johnbob",
                Email = "teste@teste.com.zz",
                RegistryDate = DateTime.Now,
                Actived = true,
                UserRoleId = 2,
                Password = Encrypt.Sha256encrypt("!Old56789"),
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                UserRole = UserRoleContributorMock()
            };
        }
        public static User UserBasicMock()
        {
            return new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Bob",
                Surname = "Silva",
                Username = "johnbobs",
                Email = "teste@teste.com.zz",
                RegistryDate = DateTime.Now,
                Actived = true,
                UserRoleId = 3,
                Password = "4365462348230523057324057325fn584305A",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                UserRole = UserRoleContributorMock()
            };
        }

        public static User UserContributorModifiedMock()
        {
            return new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "David",
                Surname = "Coffee",
                Username = "wellingtonedit",
                Email = "teste@teste.com.zz",
                RegistryDate = DateTime.Now,
                Actived = true,
                UserRoleId = 2,
                Password = "0CBAA0CB5320F4356F818F77314D4B21A74269B8B2FD7733AE37FA261B0997F2",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                UserRole = UserRoleContributorMock()
            };
        }

        public static User UserEmptyPasswordMock()
        {
            return new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "John",
                Surname = "Bob",
                Username = "johnbob",
                Email = "teste@teste.com.zz",
                RegistryDate = DateTime.Now,
                Actived = false,
                UserRoleId = 2,
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                UserRole = UserRoleContributorMock()
            };
        }

        public static User UserInactiveUserMock()
        {
            return new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "John",
                Surname = "Bob",
                Username = "johnbob",
                Email = "teste@teste.com.zz",
                RegistryDate = DateTime.Now,
                Actived = false,
                UserRoleId = 2,
                Password = "ASDGSDG464DSFSADG345435SDG",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                UserRole = UserRoleContributorMock()
            };
        }

        
        public static UserRole UserRoleContributorMock()
        {
            return new UserRole
            {
                Id = 2,
                Name = "contributor"
            };
        }

        public static UserResponse UserActivedContributorMock()
        {
            return new UserResponse
            {
                Id = Guid.NewGuid().ToString(),
                Name = "David",
                Surname = "Coffee",
                Username = "wellingtonedit",
                Email = "teste1@teste.com.zz",
                Password = "!Abc56789",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                RegistryDate = DateTime.Now,
                Actived = true,
                UserRoleId = 2
            };
        }
        public static UserResponse UserInactiveContributorMock()
        {
            return new UserResponse
            {
                Id = Guid.NewGuid().ToString(),
                Name = "David",
                Surname = "Coffee",
                Username = "wellingtonedit",
                Email = "teste1@teste.com.zz",
                Password = "!Abc56789",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                RegistryDate = DateTime.Now,
                Actived = false,
                UserRoleId = 2
            };
        }

        public static UserValidator UserValidatorErrorMock()
        {
            return new UserValidator
            {
                Errors = new List<string> {
                    "Invalid return"
                }
            };
        }
    }
}
