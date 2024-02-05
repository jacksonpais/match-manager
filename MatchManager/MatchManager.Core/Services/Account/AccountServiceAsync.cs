using AutoMapper;
using Azure.Core;
using MatchManager.Common;
using MatchManager.Core.Services.Account.Interface;
using MatchManager.Core.Services.Token.Interface;
using MatchManager.Core.Wrappers;
using MatchManager.Core.Wrappers.Interface;
using MatchManager.Domain.Entities.Account;
using MatchManager.Domain.Entities.User;
using MatchManager.Domain.Enums;
using MatchManager.DTO.Account;
using MatchManager.Infrastructure.Repositories.Account.Interface;
using MatchManager.Services.Secure;
using MatchManager.Services.SecurityService;
using MatchManager.Services.SecurityService.Interface;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;

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

        public AccountServiceAsync(IAccountRepositoryAsync accountRepository, IMapper mapper, ITokenService tokenService, ISecureService secureService, IDataProtectionProvider iDataProtectionProvider)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _secureService = secureService;
            _iDataProtectionProvider = iDataProtectionProvider;
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
                            _result.Result = CreateUserObject(loginUser);
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
            catch (Exception ex)
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

                    //var body = _emailSender.CreateRegistrationVerificationEmail(user, Properties.Resource.DomainUrl + Properties.Resource.RegistrationVerificationUrl, Properties.Resource.MainEmail);
                    //MessageTemplate messageTemplate = new MessageTemplate()
                    //{
                    //    ToAddress = user.Email,
                    //    Subject = "Welcome to MeetApp",
                    //    Body = body,
                    //    EmailProperties = new EmailProperties()
                    //    {
                    //        Email = Convert.ToString(Properties.Resource.Email).Trim(),
                    //        Password = Convert.ToString(Properties.Resource.Password).Trim(),
                    //        Host = Convert.ToString(Properties.Resource.MailHost).Trim(),
                    //        Port = Convert.ToInt32(Properties.Resource.MailPort),
                    //        DisplayName = Convert.ToString(Properties.Resource.AppName).Trim()
                    //    }
                    //};

                    //_emailSender.SendMailusingSmtp(messageTemplate);

                    _result.IsSuccess = true;
                    _result.Result = "Almost done! Your account has been created, but in order to proceed, please check your email and click the link inside to confirm your account.";
                }
                else
                {
                    _result.IsSuccess = false;
                    _result.ErrorMessages.Add("Error While Registrating User");
                }
            }
            catch (Exception ex)
            {
                _result.IsSuccess = false;
                _result.ErrorMessages.Add("Error While Registrating User");
            }
            return _result;
        }

        private UserDTO CreateUserObject(LoginUser loginUser)
        {
            return new UserDTO
            {
                FirstName = loginUser.FirstName,
                LastName = loginUser.LastName,
                UserName = loginUser.UserName,
                Token = _tokenService.CreateToken(loginUser),
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
            catch (Exception)
            {
                _result.IsSuccess = false;
                _result.ErrorMessages.Add("Sorry Verification Failed Please request a new Verification link!");
            }
            return _result;
        }
    }
}
