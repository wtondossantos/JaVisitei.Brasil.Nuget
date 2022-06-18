using JaVisitei.Brasil.Business.ViewModels.Request.Profile;

namespace JaVisitei.Brasil.Business.Validation.Test.Mocks
{
    public static class ProfileMock
    {
        public static LoginRequest LoginRequestMock()
        {
            return new LoginRequest
            {
                Email = "teste@teste.com.zz",
                Password = "!Abc5678"
            };
        }

        public static LoginRequest LoginRequestInvalidMock()
        {
            return new LoginRequest
            {
                Email = "testetestecom",
                Password = ""
            };
        }

        public static LoginRequest LoginRequestEmptyMock()
        {
            return new LoginRequest
            {
                Email = "",
                Password = ""
            };
        }

        public static ActiveAccountRequest ActiveAccountRequestMock()
        {
            return new ActiveAccountRequest
            {
                ActivationCode = "2ASFSEA5001"
            };
        }

        public static ActiveAccountRequest ActiveAccountRequestInvalidMock()
        {
            return new ActiveAccountRequest
            {
                ActivationCode = "2ASFS4A50A"
            };
        }

        public static ActiveAccountRequest ActiveAccountRequestEmptyMock()
        {
            return new ActiveAccountRequest
            {
                ActivationCode = ""
            };
        }

        public static GenerateConfirmationCodeRequest GenerateConfirmationCodeRequestMock()
        {
            return new GenerateConfirmationCodeRequest
            {
                Email = "teste@teste.com.zz"
            };
        }

        public static GenerateConfirmationCodeRequest GenerateConfirmationCodeRequestInvalidMock()
        {
            return new GenerateConfirmationCodeRequest
            {
                Email = "testetestecomzz"
            };
        }

        public static GenerateConfirmationCodeRequest GenerateConfirmationCodeRequestEmptyMock()
        {
            return new GenerateConfirmationCodeRequest
            {
                Email = ""
            };
        }

        public static ForgotPasswordRequest ForgotPasswordRequestMock()
        {
            return new ForgotPasswordRequest
            {
                Email = "teste@teste.com.zz"
            };
        }

        public static ForgotPasswordRequest ForgotPasswordRequestInvalidMock()
        {
            return new ForgotPasswordRequest
            {
                Email = "testetestecomzz"
            };
        }

        public static ForgotPasswordRequest ForgotPasswordRequestEmptyMock()
        {
            return new ForgotPasswordRequest
            {
                Email = "testetestecomzz"
            };
        }

        public static ResetPasswordRequest ResetPasswordRequestMock()
        {
            return new ResetPasswordRequest
            {
                ResetPasswordCode = "ASF325AS00",
                Email = "teste@teste.com.zz",
                Password = "!Abc5678",
                RePassword = "!Abc5678"
            };
        }

        public static ResetPasswordRequest ResetPasswordRequestInvalidMock()
        {
            return new ResetPasswordRequest
            {
                ResetPasswordCode = "ASF325AS0A",
                Email = "testetestecom",
                Password = "1234567",
                RePassword = "123456"
            };
        }

        public static ResetPasswordRequest ResetPasswordRequestEmptyMock()
        {
            return new ResetPasswordRequest
            {
                ResetPasswordCode = "",
                Email = "",
                Password = "",
                RePassword = ""
            };
        }
    }
}
