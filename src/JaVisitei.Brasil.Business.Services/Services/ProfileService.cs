using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using JaVisitei.Brasil.Business.ViewModels.Response.Profile;
using JaVisitei.Brasil.Business.ViewModels.Request.Email;
using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Helper.Others;
using JaVisitei.Brasil.Business.Enums;
using JaVisitei.Brasil.Security;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using System;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class ProfileService : BaseService<User>, IProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly ProfileValidator<LoginResponse> _profileLoginValidator;
        private readonly ProfileValidator<ActivationResponse> _profileActivationValidator;
        private readonly ProfileValidator<ForgotPasswordResponse> _profileForgotPasswordValidator;
        private readonly ProfileValidator<ResetPasswordResponse> _profileResetPasswordValidator;
        private readonly IUserManagerRepository _userManagerRepository;
        private readonly IEmailService _emailService;
        private IMapper _mapper;

        public ProfileService(IUserRepository userRepository,
            ProfileValidator<LoginResponse> profileLoginValidator,
            ProfileValidator<ActivationResponse> profileActivationValidator,
            ProfileValidator<ForgotPasswordResponse> profileForgotPasswordValidator,
            ProfileValidator<ResetPasswordResponse> profileResetPasswordValidator,
            IUserManagerRepository userManagerRepository,
            IEmailService emailService,
            IMapper mapper) : base(userRepository)
        {
            _userRepository = userRepository;
            _profileLoginValidator = profileLoginValidator;
            _userManagerRepository = userManagerRepository;
            _profileActivationValidator = profileActivationValidator;
            _profileForgotPasswordValidator = profileForgotPasswordValidator;
            _profileResetPasswordValidator = profileResetPasswordValidator;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<ProfileValidator<LoginResponse>> LoginAsync(LoginRequest request)
        {
            try
            {
                var user = _mapper.Map<User>(request);
                var result = await _userRepository.LoginAsync(user.Email, user.Password);

                if (result != null && !String.IsNullOrEmpty(result.Password))
                {
                    var token = TokenString.GenerateAuthenticationToken(result);

                    _profileLoginValidator.Message = "Login realizado com sucesso.";
                    _profileLoginValidator.Data = new LoginResponse
                    {
                        Id = result.Id,
                        Expiration = DateTime.Now.AddMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIDED_MINUTE"))),
                        Token = token
                    };

                    return _profileLoginValidator;
                }

                _profileLoginValidator.Errors.Add("Usuário ou senha inválido.");
            }
            catch (Exception ex)
            {
                _profileLoginValidator.Errors.Add($"Exception: {ex.Message}");
            }

            return _profileLoginValidator;
        }

        public async Task<ProfileValidator<ActivationResponse>> ActiveAccountAsync(string activationCode)
        {
            try
            {
                _profileActivationValidator.ValidatesConfirmationEmail(activationCode);

                if (_profileActivationValidator.IsValid)
                {
                    var userManager = await _userManagerRepository
                        .GetFirstOrDefaultAsync(x => x.ManagerCode.Equals(activationCode.Substring(0,8)) &&
                            x.Id.Equals(Convert.ToInt32(activationCode.Substring(8,activationCode.Length-8))));

                    if (userManager != null)
                    {
                        _profileActivationValidator.ValidatesExpirationDate(userManager.ExpirationDate);

                        if (!_profileActivationValidator.IsValid)
                            return _profileActivationValidator;

                        userManager.ConfirmedChange = true;
                        var userManagerResult = await _userManagerRepository.EditAsync(userManager);

                        if (userManagerResult)
                        {
                            var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Id.Equals(userManager.UserId));
                            if (user != null)
                            {
                                user.Actived = true;

                                var UserResult = await _userRepository.EditAsync(user);

                                if (UserResult)
                                {
                                    _profileActivationValidator.Message = "Perfil confirmado com sucesso.";
                                    return _profileActivationValidator;
                                }
                            }
                        }
                    }

                    _profileActivationValidator.Errors.Add("Erro ao ativar perfil.");
                }
            }
            catch (Exception ex)
            {
                _profileActivationValidator.Errors.Add($"Erro ao ativar perfil: {ex.Message}");
            }

            return _profileActivationValidator;
        }

        public async Task<ProfileValidator<ForgotPasswordResponse>> ForgotPasswordAsync(string email)
        {
            try
            {
                _profileForgotPasswordValidator.ValidatesEmail(email);

                if (!_profileForgotPasswordValidator.IsValid)
                    return _profileForgotPasswordValidator;
                
                var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email.Equals(email.ToLower()));
                if (user == null)
                {
                    _profileForgotPasswordValidator.Errors.Add("E-mail não cadastrado.");
                    return _profileForgotPasswordValidator;
                }

                var userManagerResponse = new UserManager
                {
                    UserId = user.Id,
                    EmailId = (int)EmailEnum.ForgotPassword,
                    ManagerCode = TokenString.GeneratePasswordResetToken(),
                    ExpirationDate = DateTime.Now.AddMinutes(30),
                    ConfirmedChange = false
                };
                var userManagerResult = await _userManagerRepository.AddAsync(userManagerResponse);
                string confirmarEmail = String.Empty;

                if (userManagerResult)
                {
                    var userManager = await _userManagerRepository.GetFirstOrDefaultAsync(x =>
                        x.ManagerCode.Equals(userManagerResponse.ManagerCode) &&
                        x.EmailId.Equals((int)EmailEnum.ForgotPassword) &&
                        x.UserId.Equals(user.Id), o => o.ExpirationDate);

                    if (userManager == null)
                    {
                        _profileForgotPasswordValidator.Errors.Add("Algo deu errado, tente novamente ou entre em contato com o suporte.");
                        return _profileForgotPasswordValidator;
                    }

                    var sendEmailRequest = new SendEmailRequest
                    {
                        Id = userManager.EmailId,
                        UserManagerId = userManager.Id,
                        EmailTO = user.Email,
                        ActivationCode = userManager.ManagerCode
                    };

                    var emailResult = await _emailService.SendAsync(sendEmailRequest);
                    if (emailResult.IsValid)
                        confirmarEmail = emailResult.Message;
                    else
                        confirmarEmail = emailResult.Errors.FirstOrDefault();
                }

                _profileForgotPasswordValidator.Message = $"Código enviado por e-mail {email}. {confirmarEmail}";
            }
            catch (Exception ex)
            {
                _profileForgotPasswordValidator.Errors.Add($"Erro ao solicitar trocar de senha: {ex.Message}");
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

                var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email.Equals(request.Email.ToLower()));
                if (user == null)
                {
                    _profileResetPasswordValidator.Errors.Add("E-mail não cadastrado.");
                    return _profileResetPasswordValidator;
                }

                var userManager = await _userManagerRepository
                                            .GetFirstOrDefaultAsync(x => x.UserId.Equals(user.Id) &&
                                                x.ManagerCode.Equals(request.ResetPasswordCode.Substring(0, 8)) &&
                                                x.Id.Equals(Convert.ToInt32(request.ResetPasswordCode.Substring(8, request.ResetPasswordCode.Length - 8))));

                if (userManager != null)
                {
                    _profileResetPasswordValidator.ValidatesExpirationDate(userManager.ExpirationDate);

                    if (!_profileResetPasswordValidator.IsValid)
                        return _profileResetPasswordValidator;

                    user.Password = Encrypt.Sha256encrypt(request.Password);
                    user.SecurityStamp = Guid.NewGuid().ToString();

                    var userResult = await _userRepository.EditAsync(user);
                    if (userResult)
                    {
                        userManager.ConfirmedChange = true;
                        var userManagerResult = await _userManagerRepository.EditAsync(userManager);

                        if (userManagerResult)
                        {
                            _profileResetPasswordValidator.Message = $"Senha do usuário {request.Email}, atualizada com sucesso.";
                            return _profileResetPasswordValidator;
                        }

                        _profileResetPasswordValidator.Errors.Add("Algo deu errado, tente novamente ou entre em contato com o suporte.");
                    }
                }

                _profileResetPasswordValidator.Errors.Add("Erro ao atualizar senha.");
            }
            catch (Exception ex)
            {
                _profileResetPasswordValidator.Errors.Add($"Erro ao solicitar trocar de senha: {ex.Message}");
            }

            return _profileResetPasswordValidator;
        }
    }
}
