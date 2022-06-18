using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Helper.Others;
using System;

namespace JaVisitei.Brasil.Security.Test
{
    public static class UserMock
    {
        public static User UserContributorMock()
        {
            return new User
            {
                Id = 2,
                Name = "John",
                Surname = "Bob",
                Username = "johnbob",
                Email = "teste@teste.com.zz",
                RegistryDate = DateTime.Now,
                Actived = true,
                UserRoleId = 2,
                Password = Encrypt.Sha256encrypt("OldPass123"),
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
    }
}
