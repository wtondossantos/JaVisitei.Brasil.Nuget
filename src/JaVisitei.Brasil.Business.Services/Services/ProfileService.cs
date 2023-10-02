using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using JaVisitei.Brasil.Business.ViewModels.Response.Profile;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Helper.Others;
using JaVisitei.Brasil.Security;
using System.Threading.Tasks;
using System;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserService _userService;
        private readonly ProfileValidator<LoginResponse> _profileLoginValidator;
        private readonly ProfileValidator<ActivationResponse> _profileActivationValidator;
        private readonly ProfileValidator<ForgotPasswordResponse> _profileForgotPasswordValidator;
        private readonly ProfileValidator<ResetPasswordResponse> _profileResetPasswordValidator;
        private readonly ProfileValidator<GenerateConfirmationCodeResponse> _profileGenerateConfirmationCodeValidator;
        private readonly IUserManagerService _userManagerService;
        private readonly IEmailService _emailService;

        public ProfileService(IUserService userService,
            ProfileValidator<LoginResponse> profileLoginValidator,
            ProfileValidator<ActivationResponse> profileActivationValidator,
            ProfileValidator<ForgotPasswordResponse> profileForgotPasswordValidator,
            ProfileValidator<ResetPasswordResponse> profileResetPasswordValidator,
            ProfileValidator<GenerateConfirmationCodeResponse> profileGenerateConfirmationCodeValidator,
            IUserManagerService userManagerService,
            IEmailService emailService)
        {
            _userService = userService;
            _profileLoginValidator = profileLoginValidator;
            _userManagerService = userManagerService;
            _profileActivationValidator = profileActivationValidator;
            _profileForgotPasswordValidator = profileForgotPasswordValidator;
            _profileResetPasswordValidator = profileResetPasswordValidator;
            _profileGenerateConfirmationCodeValidator = profileGenerateConfirmationCodeValidator;
            _emailService = emailService;
        }

        public async Task<ProfileValidator<LoginResponse>> LoginAsync(LoginRequest request)
        {
            try
            {
                _profileLoginValidator.ValidatesLogin(request);

                if (!_profileLoginValidator.IsValid)
                    return _profileLoginValidator;

                var result = await _userService.LoginAsync<User>(request.Email, Encrypt.Sha256encrypt(request.Password));

                if (result is null || string.IsNullOrEmpty(result.Password))
                {
                    _profileLoginValidator.Errors.Add("Usuário ou senha inválido.");
                    return _profileLoginValidator;
                }

                if (!result.Actived)
                {
                    _profileLoginValidator.Errors.Add("Usuário não confirmado, confirme o e-mail.");
                    return _profileLoginValidator;
                }

                _profileLoginValidator.Data = new LoginResponse
                {
                    Id = result.Id,
                    Expiration = DateTime.Now.AddMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIDED_MINUTE"))),
                    Token = TokenString.GenerateAuthenticationToken(result),
                    RToken = TokenString.GenerateAuthenticationRefreshToken(result),
                    RefreshToken = result.RefreshToken
                };
                _profileLoginValidator.Message = "Login realizado com sucesso.";
                
            }
            catch
            {
                _profileLoginValidator.Errors.Add("Algo deu errado. Tente novamente ou entre em contato com o suporte.");
                throw;
            }

            return _profileLoginValidator;
        }

        public async Task<ProfileValidator<LoginResponse>> RefreshTokenAsync(RefreshTokenRequest request)
        {
            try
            {
                _profileLoginValidator.ValidatesRefleshToken(request);

                if (!_profileLoginValidator.IsValid)
                    return _profileLoginValidator;

                var account = TokenString.ValidateJwtToken(request.RToken);

                var result = await _userService.RefreshTokenAsync<User>(account, request.RefreshToken);

                if (result is null)
                {
                    _profileLoginValidator.Errors.Add("Efetue o login novamente.");
                    return _profileLoginValidator;
                }

                if (!result.Actived)
                {
                    _profileLoginValidator.Errors.Add("Usuário não confirmado, confirme o e-mail.");
                    return _profileLoginValidator;
                }

                var userFound = await _userService.GetFirstOrDefaultAsync(x => x.Id.Equals(result.Id));
                if (userFound is null)
                {
                    _profileLoginValidator.Errors.Add("Efetue o login novamente.");
                    return _profileLoginValidator;
                }

                if (!userFound.SecurityStamp.Equals(result.SecurityStamp))
                {
                    _profileLoginValidator.Errors.Add("Efetue o login novamente.");
                    return _profileLoginValidator;
                }

                result.RefreshToken = Guid.NewGuid().ToString();
                result.RefreshTokenDate = DateTime.Now;
                if (!await _userService.UpdateAsync(result))
                {
                    _profileLoginValidator.Errors.Add("Efetue o login novamente.");
                    return _profileLoginValidator;
                }

                _profileLoginValidator.Data = new LoginResponse
                {
                    Id = result.Id,
                    Expiration = DateTime.Now.AddMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIDED_MINUTE"))),
                    Token = TokenString.GenerateAuthenticationToken(result),
                    RToken = TokenString.GenerateAuthenticationRefreshToken(result),
                    RefreshToken = result.RefreshToken
                };

                _profileLoginValidator.Message = "Login realizado com sucesso.";
            }
            catch
            {
                _profileLoginValidator.Errors.Add("Algo deu errato. Tente novamente ou entre em contato com o suporte.");
                throw;
            }

            return _profileLoginValidator;
        }

        public async Task<ProfileValidator<ActivationResponse>> ActiveAccountAsync(ActiveAccountRequest request)
        {
            try
            {
                _profileActivationValidator.ValidatesConfirmationEmail(request);

                if (!_profileActivationValidator.IsValid)
                    return _profileActivationValidator;

                var userManager = await _userManagerService.GetByManagerCodeAsync(request.ActivationCode);
                if (userManager is null)
                {
                    _profileActivationValidator.Errors.Add("Informe um código válido.");
                    return _profileActivationValidator;
                }

                _profileActivationValidator.ValidatesEmailConfirmationCodeExpirationTime(userManager.ExpirationDate);

                if (!_profileActivationValidator.IsValid)
                    return _profileActivationValidator;

                if (await _userManagerService.ConfirmedChangeAsync(userManager))
                {
                    var user = await _userService.GetFirstOrDefaultAsync(x => x.Id.Equals(userManager.UserId));
                    if (user is null)
                    {
                        _profileActivationValidator.Errors.Add("Erro ao consultar usuário.");
                        return _profileActivationValidator;
                    }

                    user.Actived = true;

                    if (await _userService.UpdateAsync(user))
                    {
                        _profileActivationValidator.Data = new ActivationResponse { 
                            Actived = true,
                            UserEmail = user.Email
                        };
                        _profileActivationValidator.Message = "Perfil confirmado com sucesso.";
                    }
                    else
                        _profileActivationValidator.Errors.Add("Erro ao ativar perfil.");
                }
                else
                    _profileActivationValidator.Errors.Add("Erro ao ativar perfil.");
            }
            catch
            {
                _profileActivationValidator.Errors.Add("Erro ao ativar perfil. Tente novamente ou entre em contato com o suporte.");
                throw;
            }

            return _profileActivationValidator;
        }

        public async Task<ProfileValidator<GenerateConfirmationCodeResponse>> GenerateConfirmationCodeAsync(GenerateConfirmationCodeRequest request)
        {
            try
            {
                _profileGenerateConfirmationCodeValidator.ValidatesEmail(request);

                if (!_profileGenerateConfirmationCodeValidator.IsValid)
                    return _profileGenerateConfirmationCodeValidator;

                var user = await _userService.GetFirstOrDefaultAsync(x => x.Email.Equals(request.Email.ToLower()));
                if (user is null)
                {
                    _profileGenerateConfirmationCodeValidator.Errors.Add("Não existe perfil cadastrado com esse e-mail. Crie uma conta.");
                    return _profileGenerateConfirmationCodeValidator;
                }
                else if (user.Actived)
                {
                    _profileGenerateConfirmationCodeValidator.Message = "Este e-mail já foi confirmado e sua conta está ativa. Efetue o Login.";
                    return _profileGenerateConfirmationCodeValidator;
                }

                var userManager = await _userManagerService.CreateEmailConfirmationAsync(user.Id);
                if (userManager is null)
                {
                    _profileGenerateConfirmationCodeValidator.Errors.Add("Erro ao adicionar configuração do usuário, tente novamente ou entre em contato com o suporte.");
                    return _profileGenerateConfirmationCodeValidator;
                }
                
                userManager.User = user;
                var emailResult = await _emailService.SendEmailUserManagerAsync(userManager);
                if (emailResult.IsValid)
                {
                    _profileGenerateConfirmationCodeValidator.Data = new GenerateConfirmationCodeResponse { 
                        Generated = true,
                        UserEmail = userManager.User.Email
                    };
                    _profileGenerateConfirmationCodeValidator.Message = $"Código de confirmação gerado com suscesso. {emailResult.Message}";
                }
                else
                    _profileGenerateConfirmationCodeValidator.Errors = emailResult.Errors;
            }
            catch
            {
                _profileGenerateConfirmationCodeValidator.Errors.Add("Erro ao solicitar código de confirmação. Tente novamente ou entre em contato com o suporte.");
                throw;
            }

            return _profileGenerateConfirmationCodeValidator;
        }

        public async Task<ProfileValidator<ForgotPasswordResponse>> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            try
            {
                _profileForgotPasswordValidator.ValidatesEmail(request);

                if (!_profileForgotPasswordValidator.IsValid)
                    return _profileForgotPasswordValidator;

                var user = await _userService.GetFirstOrDefaultAsync(x => x.Email.Equals(request.Email.ToLower()));
                if (user is null)
                {
                    _profileForgotPasswordValidator.Errors.Add("E-mail não cadastrado.");
                    return _profileForgotPasswordValidator;
                }

                var userManager = await _userManagerService.CreatePasswordResetAsync(user.Id);
                if (userManager is null)
                {
                    _profileForgotPasswordValidator.Errors.Add("Erro ao adicionar configuração de usuário, tente novamente ou entre em contato com o suporte.");
                    return _profileForgotPasswordValidator;
                }

                userManager.User = user;
                var emailResult = await _emailService.SendEmailUserManagerAsync(userManager);
                if (emailResult.IsValid)
                {
                    _profileForgotPasswordValidator.Data = new ForgotPasswordResponse { 
                        Requested = true,
                        UserEmail = user.Email
                    };
                    _profileForgotPasswordValidator.Message = emailResult.Message;
                }
                else
                    _profileForgotPasswordValidator.Errors = emailResult.Errors;
            }
            catch
            {
                _profileForgotPasswordValidator.Errors.Add("Erro ao solicitar trocar de senha. tente novamente ou entre em contato com o suporte.");
                throw;
            }

            return _profileForgotPasswordValidator;
        }

        public async Task<ProfileValidator<ResetPasswordResponse>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            try
            {
                _profileResetPasswordValidator.ValidatesResetPassword(request);

                if (!_profileResetPasswordValidator.IsValid)
                    return _profileResetPasswordValidator;

                var user = await _userService.GetFirstOrDefaultAsync(x => x.Email.Equals(request.Email.ToLower()));
                if (user is null)
                {
                    _profileResetPasswordValidator.Errors.Add("E-mail não cadastrado.");
                    return _profileResetPasswordValidator;
                }

                var userManager = await _userManagerService.GetByManagerCodeAsync(request.ResetPasswordCode);
                if (userManager is null)
                {
                    _profileResetPasswordValidator.Errors.Add("Erro ao atualizar senha. Tente novemente ou entre em contato com o suporte.");
                    return _profileResetPasswordValidator;
                }

                _profileResetPasswordValidator.ValidatesPasswordConfirmationCodeExpirationTime(userManager.ExpirationDate);

                if (!_profileResetPasswordValidator.IsValid)
                    return _profileResetPasswordValidator;

                if (userManager.ConfirmedChange)
                {
                    _profileResetPasswordValidator.Message = "Código de atualização já utilizado, solicite novo código em: Esqueci minha senha.";
                    return _profileResetPasswordValidator;
                }

                user.Password = Encrypt.Sha256encrypt(request.Password);
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.Actived = true;

                if (await _userService.UpdateAsync(user))
                {
                    if (await _userManagerService.ConfirmedChangeAsync(userManager))
                    {
                        _profileResetPasswordValidator.Data = new ResetPasswordResponse {
                            Redefined = true,
                            UserEmail = request.Email
                        };
                        _profileResetPasswordValidator.Message = $"Senha do usuário {request.Email}, atualizada com sucesso.";
                    }
                    else
                        _profileResetPasswordValidator.Errors.Add("Algo deu errado, tente novamente ou entre em contato com o suporte.");
                }
                else
                    _profileResetPasswordValidator.Errors.Add("Erro ao atualizar senha.");
            }
            catch
            {
                _profileResetPasswordValidator.Errors.Add("Erro ao solicitar trocar de senha. Tente novemente ou entre em contato com o suporte.");
                throw;
            }

            return _profileResetPasswordValidator;
        }
    }
}