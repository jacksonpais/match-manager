using MatchManager.Data.Context;
using MatchManager.Domain.Entities.Account;
using MatchManager.Domain.Enums;
using MatchManager.Infrastructure.Repositories.Account.Interface;

namespace MatchManager.Infrastructure.Repositories.Account
{
    public class AccountRepositoryAsync : IAccountRepositoryAsync
    {
        private readonly DataContext _db;
        public AccountRepositoryAsync(DataContext db)
        {
            _db = db;
        }

        public UserActivation GetActivation(AppUserMaster user, ActivationType activationType)
        {
            throw new NotImplementedException();
        }

        public AppUserMaster GetUser(long userid)
        {
            throw new NotImplementedException();
        }

        public AppUserMaster GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public string GetUserSaltbyUserid(long userid)
        {
            throw new NotImplementedException();
        }

        public bool IsUserActivated(long userid)
        {
            throw new NotImplementedException();
        }

        public bool IsUserPresent(string email)
        {
            bool rtn = false;
            var user = _db.AppUserMaster.FirstOrDefault(user => user.Email.ToLower() == email.ToLower());
            if (user != null) 
            {
                rtn = true;
            }
            return rtn;
        }

        public void SaveActivation(UserActivation activation)
        {
            throw new NotImplementedException();
        }

        public void SaveUser(AppUserMaster user)
        {
            throw new NotImplementedException();
        }

        public void SaveUserToken(UserToken userTokens)
        {
            throw new NotImplementedException();
        }
    }
}
