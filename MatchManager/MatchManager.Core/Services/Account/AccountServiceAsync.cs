using AutoMapper;
using MatchManager.Common;
using MatchManager.Core.Services.Account.Interface;
using MatchManager.Core.Wrappers;
using MatchManager.Core.Wrappers.Interface;
using MatchManager.Domain.Entities.Account;
using MatchManager.Domain.Enums;
using MatchManager.DTO.Account;
using MatchManager.Infrastructure.Repositories.Account.Interface;
using Microsoft.Win32;

namespace MatchManager.Core.Services.Account
{
    public class AccountServiceAsync : IAccountServiceAsync
    {
        private readonly IAccountRepositoryAsync _accountRepository;
        protected CoreResult _result;
        private readonly IMapper _mapper;

        public AccountServiceAsync(IAccountRepositoryAsync accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _result = new CoreResult();
        }

        public bool IsUserPresent(string username)
        {
            return _accountRepository.IsUserPresent(username);
        }

        public Task<CoreResult> Login(LoginRequestDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<CoreResult> Register(RegisterRequestDTO registerDTO)
        {
            var salt = GenerateRandomNumbers.GenerateRandomDigitCode(20);
            var saltedpassword = HashHelper.CreateHashSHA512(registerDTO.Password, salt);

            AppUserMaster userMappedobject = _mapper.Map<AppUserMaster>(registerDTO);
            userMappedobject.PasswordHash = saltedpassword;
            userMappedobject.CreatedDate = DateTime.Now;
            userMappedobject.UpdatedDate = DateTime.Now;
            userMappedobject.MobileNo = "";
            userMappedobject.UserName = registerDTO.Email;
            userMappedobject.Initial = registerDTO.FirstName.Substring(0, 1) + registerDTO.LastName.Substring(0, 1);

            await _accountRepository.SaveUser(userMappedobject);
            long userid = _accountRepository.GetUserId(userMappedobject.Email);

            if (userid != 0)
            {
                var user = _accountRepository.LoginUser(userid);

                UserToken userTokens = new UserToken()
                {
                    UserId = userid,
                    HashId = 0,
                    PasswordSalt = salt,
                    TokenId = user.UserToken.TokenId
                };

                var token = HashHelper.CreateHashSHA256((GenerateRandomNumbers.GenerateRandomDigitCode(6)));
                _accountRepository.SaveUserToken(userTokens);
                UserActivation userActivation = _accountRepository.GetActivation(user, ActivationType.email);
                userActivation.ActivationToken = token;
                _accountRepository.SaveActivation(userActivation);
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
            return _result;
        }
    }
}
