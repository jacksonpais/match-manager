using AutoMapper;
using MatchManager.Data.Context;
using MatchManager.Domain.Entities.Account;
using MatchManager.Domain.Entities.User;
using MatchManager.Domain.Enums;
using MatchManager.Infrastructure.Repositories.Account.Interface;
using System.Data;

namespace MatchManager.Infrastructure.Repositories.Account
{
    public class AccountRepositoryAsync : IAccountRepositoryAsync
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        public AccountRepositoryAsync(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public AppUserMaster GetUser(long userid)
        {
            return _db.AppUserMaster.FirstOrDefault(user => user.UserId == userid);
        }

        public AppUserMaster GetUser(string username)
        {
            return _db.AppUserMaster.FirstOrDefault(user => user.Email.ToLower() == username.ToLower());
        }

        public UserActivation GetUserActivation(long userid, ActivationType activationType)
        {
            return _db.UserActivation.FirstOrDefault(activation => activation.UserId == userid && activationType == activationType);
        }

        public List<UserActivation> GetUserActivations(long userid)
        {
            return _db.UserActivation.Where(activation => activation.UserId == userid).ToList();
        }

        public long GetUserId(string username)
        {
            long userid = 0;
            var user = GetUser(username);
            if (user != null)
            {
                userid = user.UserId;
            }
            return userid;
        }

        public string GetUserSaltbyUserid(long userid)
        {
            return LoginUser(userid).UserToken.PasswordSalt;
        }

        public UserToken GetUserToken(long userid)
        {
            return LoginUser(userid).UserToken;
        }

        public bool IsUserPresent(string username)
        {
            bool rtn = false;
            var userid = GetUserId(username);
            if (userid != 0)
            {
                rtn = true;
            }
            return rtn;
        }

        public LoginUser LoginUser(long userid)
        {
            if (userid != 0)
            {
                AppUserMaster user = GetUser(userid);
                UserToken userToken = GetUserToken(userid);
                List<UserActivation> userActivations = GetUserActivations(userid);
                user.UserActivation = userActivations;
                user.UserToken = userToken;
                return _mapper.Map<LoginUser>(user);
            }
            else
            {
                return null;
            }
        }

        public async Task SaveChangesToDBAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void SaveUser(AppUserMaster user)
        {
            _db.AppUserMaster.Add(user);
        }

        public void SaveUserActivation(List<UserActivation> activations)
        {
            for (int i = 0; i < activations.Count; i++)
            {
                _db.UserActivation.Add(activations[i]);
            }
        }

        public void SaveUserToken(UserToken userTokens)
        {
            _db.UserToken.Add(userTokens);
        }
    }
}
