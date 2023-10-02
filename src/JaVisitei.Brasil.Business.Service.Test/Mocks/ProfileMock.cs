using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using JaVisitei.Brasil.Business.ViewModels.Response.Profile;
using System.Collections.Generic;
using System;

namespace JaVisitei.Brasil.Business.Service.Test.Mocks
{
    public static class ProfileMock
    {
        public static ProfileValidator<ActivationResponse> ProfileActivationResponseMock()
        {
            return new ProfileValidator<ActivationResponse>
            {
                Data = new ActivationResponse
                {
                    Actived = true,
                    UserEmail = "teste@teste.com"
                },
                Errors = new List<string>()
            };
        }

        public static ProfileValidator<GenerateConfirmationCodeResponse> ProfileGenerateConfirmationCodeResponseMock()
        {
            return new ProfileValidator<GenerateConfirmationCodeResponse>
            {
                Data = new GenerateConfirmationCodeResponse
                {
                    Generated = true,
                    UserEmail = "teste@teste.com.zz"
                },
                Errors = new List<string>()
            };
        }

        public static ProfileValidator<ForgotPasswordResponse> ProfileForgotPasswordResponseMock()
        {
            return new ProfileValidator<ForgotPasswordResponse>
            {
                Data = new ForgotPasswordResponse
                {
                    Requested = true,
                    UserEmail = "teste@teste.com.zz"
                },
                Errors = new List<string>()
            };
        }

        public static ProfileValidator<ResetPasswordResponse> ProfileResetPasswordResponseMock()
        {
            return new ProfileValidator<ResetPasswordResponse>
            {
                Data = new ResetPasswordResponse
                {
                    Redefined = true,
                    UserEmail = "teste@teste.com.zz"
                },
                Errors = new List<string>()
            };
        }

        public static ProfileValidator<LoginResponse> ProfileLoginResponseMock()
        {
            return new ProfileValidator<LoginResponse>
            {
                Data = new LoginResponse
                {
                    Id = Guid.NewGuid().ToString(),
                    Token = "nfpasdngasASDFnasbsdfSADFA3453256et.ASDgasdgsadobh",
                    Expiration = DateTime.Now.AddMinutes(10)
                },
                Errors = new List<string>(),
                Message = "sucesso"
            };
        }

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
                Password = "123456"
            };
        }

        public static ProfileValidator<M> ProfileValidatorErrorsMock<M>()
        {
            return new ProfileValidator<M>
            {
                Errors = new List<string>()
            };
        }

        public static ProfileValidator<M> ProfileValidatorErrorMock<M>()
        {
            return new ProfileValidator<M>
            {
                Errors = new List<string> {
                    "Invalid return"
                }
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
    }
}
