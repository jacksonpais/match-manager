using AutoMapper;
using MatchManager.Common;
using MatchManager.Core.Services.Account.Interface;
using MatchManager.Core.Services.Token.Interface;
using MatchManager.Core.Wrappers;
using MatchManager.Domain.Config;
using MatchManager.Domain.Entities.Account;
using MatchManager.Domain.Entities.User;
using MatchManager.Domain.Enums;
using MatchManager.DTO.Account;
using MatchManager.Infrastructure.Repositories.Account.Interface;
using MatchManager.Services.Email.Interface;
using MatchManager.Services.Email.Model;
using MatchManager.Services.Secure;
using MatchManager.Services.SecurityService.Interface;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Web;
using System;

namespace MatchManager.Core.Services.Account
{
    public class AccountServiceAsync : IAccountServiceAsync
    {
        private readonly IAccountRepositoryAsync _accountRepository;
        protected CoreResult _result;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly ISecureService _secureService;
        private readonly IDataProtectionProvider _iDataProtectionProvider;
        private readonly IEmailService _emailService;
        private readonly IOptions<AppConfig> _appConfig;

        public AccountServiceAsync(IAccountRepositoryAsync accountRepository, IMapper mapper, ITokenService tokenService, ISecureService secureService, IDataProtectionProvider iDataProtectionProvider, IEmailService emailService, IConfiguration configuration, IOptions<AppConfig> appConfig)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _secureService = secureService;
            _iDataProtectionProvider = iDataProtectionProvider;
            _emailService = emailService;
            _appConfig = appConfig;
            _result = new CoreResult();
        }

        public async Task<bool> IsUserPresent(string username)
        {
            return await _accountRepository.IsUserPresent(username);
        }

        public async Task<CoreResult> Login(LoginRequestDTO request)
        {
            try
            {
                AppUserMaster user = await _accountRepository.GetUser(request.Email);
                if (user.UserId == 0)
                {
                    _result.IsSuccess = false;
                    _result.ErrorMessages.Add("Entered email is invalid");
                }
                else
                {
                    var usersalt = await _accountRepository.GetUserSaltbyUserid(user.UserId);
                    if (usersalt == null)
                    {
                        _result.IsSuccess = false;
                        _result.ErrorMessages.Add("Entered Email or Password is Invalid");
                    }
                    UserActivation userActivation = await _accountRepository.GetUserActivation(user.UserId, ActivationType.email);
                    if (userActivation.IsActive == false)
                    {
                        _result.IsSuccess = false;
                        _result.ErrorMessages.Add("Your Account is In-Active. Contact Administrator");
                    }
                    else
                    {
                        var generatedhash = HashHelper.CreateHashSHA512(request.Password, usersalt);

                        if (string.Equals(user.PasswordHash, generatedhash, StringComparison.Ordinal))
                        {
                            LoginUser loginUser = await _accountRepository.LoginUser(user.UserId);
                            _result.Result = CreateTokenObject(loginUser);
                            _result.IsSuccess = true;
                        }
                        else
                        {
                            _result.IsSuccess = false;
                            _result.ErrorMessages.Add("Entered Email or Password is Invalid");
                        }
                    }
                }
                return _result;
            }
            catch
            {
                _result.IsSuccess = false;
                _result.ErrorMessages.Add("Error While Registrating User");
            }
            return _result;
        }

        public async Task<CoreResult> Register(RegisterRequestDTO registerDTO)
        {
            try
            {
                var salt = GenerateRandomNumbers.GenerateRandomDigitCode(20);
                var saltedpassword = HashHelper.CreateHashSHA512(registerDTO.Password, salt);

                AppUserMaster appUser = _mapper.Map<AppUserMaster>(registerDTO);
                appUser.PasswordHash = saltedpassword;
                appUser.MobileNo = "";
                appUser.UserName = registerDTO.Email;
                appUser.Initial = registerDTO.FirstName.Substring(0, 1) + registerDTO.LastName.Substring(0, 1);

                _accountRepository.SaveUser(appUser);
                await _accountRepository.SaveChangesToDBAsync();
                appUser.UserId = await _accountRepository.GetUserId(registerDTO.Email);

                if (appUser.UserId != 0)
                {
                    UserToken userToken = new UserToken()
                    {
                        UserId = appUser.UserId,
                        HashId = 0,
                        PasswordSalt = salt,
                    };

                    var emailToken = HashHelper.CreateHashSHA256((GenerateRandomNumbers.GenerateRandomDigitCode(6)));
                    List<UserActivation> activations = new List<UserActivation>()
                    {
                        new UserActivation()
                        {
                            ActivationDate = DateTime.Now,
                            IsActive = false,
                            UserId = appUser.UserId,
                            TokenType = ActivationType.email.ToString(),
                            ActivationToken = emailToken
                        },
                        new UserActivation()
                        {
                            ActivationDate = DateTime.Now,
                            IsActive = false,
                            UserId = appUser.UserId,
                            TokenType = ActivationType.password.ToString(),
                            ActivationToken = ""
                        },
                        new UserActivation()
                        {
                            ActivationDate = DateTime.Now,
                            IsActive = false,
                            UserId = appUser.UserId,
                            TokenType = ActivationType.mobile.ToString(),
                            ActivationToken = ""
                        }

                    };

                    _accountRepository.SaveUserToken(userToken);
                    _accountRepository.SaveUserActivation(activations);
                    await _accountRepository.SaveChangesToDBAsync();

                    var body = _emailService.CreateRegistrationVerificationEmail(appUser, _appConfig.Value.Urls.DomainUrl + _appConfig.Value.Urls.RegistrationVerificationUrl, _appConfig.Value.EmailConfiguration.Email);
                    MessageTemplate messageTemplate = new MessageTemplate()
                    {
                        ToAddress = appUser.Email,
                        Subject = "Welcome to MeetApp",
                        Body = body,
                        Bcc = new List<string>(),
                        Cc = new List<string>(),
                        EmailProperties = new EmailProperties()
                        {
                            Email = _appConfig.Value.EmailConfiguration.Email.Trim(),
                            Password = _appConfig.Value.EmailConfiguration.Password.Trim(),
                            Host = _appConfig.Value.EmailConfiguration.MailHost.Trim(),
                            Port = _appConfig.Value.EmailConfiguration.MailPort,
                            DisplayName = _appConfig.Value.AppName.Trim()
                        }
                    };

                    await _emailService.SendEmail(messageTemplate);

                    _result.IsSuccess = true;
                    _result.Result = "Your account has been created, in order to proceed, please check your email and click the link inside to confirm your account.";
                }
                else
                {
                    _result.IsSuccess = false;
                    _result.ErrorMessages.Add("Error While Registrating User");
                }
            }
            catch
            {
                _result.IsSuccess = false;
                _result.ErrorMessages.Add("Error While Registrating User");
            }
            return _result;
        }

        private TokenDTO CreateTokenObject(LoginUser loginUser)
        {
            return new TokenDTO
            {
                AccessToken = _tokenService.CreateToken(loginUser),
            };
        }

        public async Task<CoreResult> VerifyAccount(VerifyAccountDTO request)
        {
            try
            {
                var arrayValue = SecurityHelper.SplitToken(request.Key);
                if (arrayValue != null)
                {
                    var userId = Convert.ToInt64(arrayValue[1]);
                    UserActivation userActivation = await _accountRepository.GetUserActivation(userId, ActivationType.email);
                    if (userActivation != null)
                    {
                        var result = SecurityHelper.IsTokenValid(arrayValue, request.HashToken, userActivation.ActivationToken);

                        if (result == 1)
                        {
                            _result.IsSuccess = false;
                            _result.ErrorMessages.Add("Token is invalid");
                        }

                        if (result == 2)
                        {
                            _result.IsSuccess = false;
                            _result.ErrorMessages.Add("Verification link has expired");
                        }

                        if (result == 0)
                        {
                            userActivation.IsActive = true;
                            _accountRepository.SaveUserActivation(userActivation);
                            await _accountRepository.SaveChangesToDBAsync();
                            userActivation = await _accountRepository.GetUserActivation(userId, ActivationType.email);
                            if (userActivation.IsActive)
                            {
                                _result.IsSuccess = true;
                            }
                            else
                            {
                                _result.IsSuccess = false;
                                _result.ErrorMessages.Add("Your Account is In-Active. Contact Administrator");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _result.IsSuccess = false;
                _result.ErrorMessages.Add("Sorry Verification Failed Please request a new Verification link!");
            }
            return _result;
        }

        public async Task<CoreResult> RequestVerificationLink(RequestVericationLinkDTO request)
        {
            try
            {
                AppUserMaster appUser = await _accountRepository.GetUser(request.UserName);
                if (appUser.UserId != 0)
                {
                    LoginUser login = await _accountRepository.LoginUser(appUser.UserId);
                    var emailToken = HashHelper.CreateHashSHA256((GenerateRandomNumbers.GenerateRandomDigitCode(6)));
                    Enum.TryParse(request.VerificationType, out ActivationType activationType);
                    UserActivation activation = await _accountRepository.GetUserActivation(appUser.UserId, activationType);
                    activation.ActivationToken = emailToken;
                    activation.IsActive = false;

                    _accountRepository.SaveUserActivation(activation);
                    await _accountRepository.SaveChangesToDBAsync();

                    AESAlgorithm aesAlgorithm = new AESAlgorithm();
                    var key = string.Join(":", new string[] { DateTime.Now.Ticks.ToString(), appUser.UserId.ToString() });
                    var encrypt = aesAlgorithm.EncryptToBase64String(key);
                    var linktoverify = $"{_appConfig.Value.Urls.DomainUrl + "account/verify"}?key={HttpUtility.UrlEncode(encrypt)}&hashtoken={HttpUtility.UrlEncode(login.Activations.Where(a => a.TokenType == Convert.ToString(ActivationType.email)).FirstOrDefault().ActivationToken)}";

                    _result.IsSuccess = true;
                    _result.Result = linktoverify;
                }
                else
                {
                    _result.IsSuccess = false;
                    _result.ErrorMessages.Add("Error While fetching activation link");
                }
            }
            catch
            {
                _result.IsSuccess = false;
                _result.ErrorMessages.Add("Error While fetching activation link");
            }
            return _result;
        }
    }
}
