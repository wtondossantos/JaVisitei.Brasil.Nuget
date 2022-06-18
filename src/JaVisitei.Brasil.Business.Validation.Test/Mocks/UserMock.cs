using JaVisitei.Brasil.Business.ViewModels.Request.User;

namespace JaVisitei.Brasil.Business.Validation.Test.Mocks
{
    public static class UserMock
    {
        public static InsertFullUserRequest CreateUserRequestMock()
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
                Name = "R2-D2",
                Surname = "C-3P0",
                Username = "wellington s",
                Email = "testetestecom.zz",
                ReEmail = "testetestecom.zz",
                Password = "12345",
                RePassword = "12345",
                UserRoleId = 0
            };
        }

        public static InsertFullUserRequest CreateUserInvalidEmailAndPasswordRequestMock()
        {
            return new InsertFullUserRequest
            {
                Name = "Wellington",
                Surname = "Silva",
                Username = "wellington",
                Email = "teste@teste.comzz",
                ReEmail = "teste@teste.com.zz",
                Password = "!Abc567",
                RePassword = "!Abc5678",
                UserRoleId = 3
            };
        }

        public static InsertFullUserRequest CreateUserInvalidMaxLengthRequestMock()
        {
            return new InsertFullUserRequest
            {
                Name = "Isabel Cristina Leopoldina Augusta Micaela Gabriela Rafaela Gonzaga",
                Surname = "Adolph Blaine Charles David Earl Frederick Gerald Hubert Irvin John Kenneth Lloyd Martin Nero Oliver Paul Quincy Randolph Sherman Thomas Uncas Victor William Xerxes Yancy Zeus Wolfe­schlegel­stein­hausen­berger­dorff­welche­vor­altern­waren",
                Username = "IsabelCristinaLeopoldinaAugustaMicaelaGabrielaRafaelaGonzaga",
                Email = "IsabelCristinaLeopoldinaAugustaMicaelaGabrielaRafaelaGonzaga1@IsabelCristinaLeopoldinaAugustaMicaelaGabrielaRafaelaGonzaga.IsabelCristinaLeopoldinaAugustaMicaelaGabrielaRafaela.IsabelCristinaLeopoldina",
                ReEmail = "IsabelCristinaLeopoldinaAugustaMicaelaGabrielaRafaelaGonzaga1@IsabelCristinaLeopoldinaAugustaMicaelaGabrielaRafaelaGonzaga.IsabelCristinaLeopoldinaAugustaMicaelaGabrielaRafaela.IsabelCristinaLeopoldina",
                Password = "12345678901234567890123456789#$%&$12345678901234551",
                RePassword = "12345678901234567890123456789#$%&$12345678901234551",
                UserRoleId = 0
            };
        }

        public static InsertFullUserRequest CreateUserInvalidMinLengthRequestMock()
        {
            return new InsertFullUserRequest
            {
                Name = "Pi",
                Surname = "",
                Username = "pi",
                Email = "a@b.",
                ReEmail = "a@b.",
                Password = "12bA@67",
                RePassword = "12bA@67",
                UserRoleId = 0
            };
        }

        public static InsertFullUserRequest CreateUserEmptyRequestMock()
        {
            return new InsertFullUserRequest
            {
                Name = "",
                Surname = "",
                Username = "",
                Email = "",
                ReEmail = "",
                Password = "",
                RePassword = "",
                UserRoleId = 0
            };
        }

        public static UpdateFullUserRequest UpdateUserRequestMock()
        {
            return new UpdateFullUserRequest
            {
                Id = 3,
                Name = "David",
                Surname = "Coffee",
                Username = "wellingtonedit",
                Email = "teste1@teste.com.zz",
                ReEmail = "teste1@teste.com.zz",
                OldPassword = "OldPass123",
                Password = "!Abc56789",
                RePassword = "!Abc56789",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                UserRoleId = 2
            };
        }

        public static UpdateFullUserRequest UpdateUserInvalidRequestMock()
        {
            return new UpdateFullUserRequest
            {
                Id = 3,
                Name = "R2-D2",
                Surname = "C-3P0",
                Username = "wellingtonedit f",
                Email = "teste1testecozz",
                ReEmail = "teste1testecozz",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                OldPassword = "1",
                Password = "abc56789",
                RePassword = "abc56789",
                UserRoleId = 0
            };
        }

        public static UpdateFullUserRequest UpdateUserEmptyRequestMock()
        {
            return new UpdateFullUserRequest
            {
                Id = 0,
                Name = "",
                Surname = "",
                Username = "",
                Email = "",
                ReEmail = "",
                SecurityStamp = "",
                OldPassword = "",
                Password = "Abc56789",
                RePassword = "",
                UserRoleId = 0
            };
        }

        public static UpdateFullUserRequest UpdateUserInvalidEmailAndPasswordRequestMock()
        {
            return new UpdateFullUserRequest
            {
                Id = 3,
                Name = "David",
                Surname = "Coffee",
                Username = "wellingtonedit",
                Email = "teste1teste.com.zz",
                ReEmail = "teste1@teste.com.zz",
                OldPassword = "OldPass123",
                Password = "Abc56789",
                RePassword = "!Abc56789",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                UserRoleId = 2
            };
        }

        public static UpdateFullUserRequest UpdateUserInvalidMaxLengthRequestMock()
        {
            return new UpdateFullUserRequest
            {
                Id = 3,
                Name = "Isabel Cristina Leopoldina Augusta Micaela Gabriela Rafaela Gonzaga",
                Surname = "Adolph Blaine Charles David Earl Frederick Gerald Hubert Irvin John Kenneth Lloyd Martin Nero Oliver Paul Quincy Randolph Sherman Thomas Uncas Victor William Xerxes Yancy Zeus Wolfe­schlegel­stein­hausen­berger­dorff­welche­vor­altern­waren",
                Username = "IsabelCristinaLeopoldinaAugustaMicaelaGabrielaRafaelaGonzaga",
                Email = "IsabelCristinaLeopoldinaAugustaMicaelaGabrielaRafaelaGonzaga1@IsabelCristinaLeopoldinaAugustaMicaelaGabrielaRafaelaGonzaga.IsabelCristinaLeopoldinaAugustaMicaelaGabrielaRafaela.IsabelCristinaLeopoldina",
                ReEmail = "IsabelCristinaLeopoldinaAugustaMicaelaGabrielaRafaelaGonzaga1@IsabelCristinaLeopoldinaAugustaMicaelaGabrielaRafaelaGonzaga.IsabelCristinaLeopoldinaAugustaMicaelaGabrielaRafaela.IsabelCristinaLeopoldina",
                OldPassword = "12345678901234567890123456789#$%&$12345678901234551",
                Password = "12345678901234567890123456789#$%&$12345678901234551",
                RePassword = "12345678901234567890123456789#$%&$12345678901234551",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                UserRoleId = 2
            };
        }

        public static UpdateFullUserRequest UpdateUserInvalidMinLengthRequestMock()
        {
            return new UpdateFullUserRequest
            {
                Id = 3,
                Name = "Pi",
                Surname = "",
                Username = "pi",
                Email = "a@b.",
                ReEmail = "a@b.",
                Password = "12bA@67",
                RePassword = "12bA@67",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                UserRoleId = 0
            };
        }
    }
}
