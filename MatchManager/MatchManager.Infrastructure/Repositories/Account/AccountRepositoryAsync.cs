using MatchManager.Data.Context;
using MatchManager.Domain.Entities.Account;
using MatchManager.Domain.Entities.User;
using MatchManager.Domain.Enums;
using MatchManager.Infrastructure.Repositories.Account.Interface;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MatchManager.Infrastructure.Repositories.Account
{
    public class AccountRepositoryAsync : IAccountRepositoryAsync
    {
        private readonly DataContext _db;
        public AccountRepositoryAsync(DataContext db)
        {
            _db = db;
        }

        public bool CheckIfEmailActivated(long userid)
        {
            throw new NotImplementedException();
        }

        public UserActivation GetActivation(LoginUser user, ActivationType activationType)
        {
            throw new NotImplementedException();
        }

        public AppUserMaster GetUser(long userid)
        {
            throw new NotImplementedException();
        }

        public string GetUserSaltbyUserid(LoginUser user)
        {
            throw new NotImplementedException();
        }

        public long GetUserId(string email)
        {
            long userid = 0;
            var user = _db.AppUserMaster.FirstOrDefault(user => user.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                userid = user.UserId;
            }
            return userid;
        }

        public bool IsUserPresent(string email)
        {
            bool rtn = false;
            var userid = GetUserId(email);
            if (userid != 0)
            {
                rtn = true;
            }
            return rtn;
        }

        public LoginUser LoginUser(long userid)
        {
            var appUser = _db.AppUserMaster.FirstOrDefault(user => user.UserId == userid);
            LoginUser loginUser = new LoginUser()
            {
                UserId = appUser.UserId,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Email = appUser.Email,
                PasswordHash = appUser.PasswordHash,
                IsFirstTimeLoggedInUser = appUser.IsFirstTimeLoggedInUser
            };
            var appToken = _db.UserToken.FirstOrDefault(token => token.UserId == userid);
            //loginUser.UserToken = new UserToken()
            //{
            //    TokenId = appUser.,
            //    HashId = Convert.ToInt32(row["hashid"]),
            //    TokenDate = Convert.ToString(row["tokendate"]),
            //    PasswordSalt = Convert.ToString(row["passwordsalt"])
            //};
            var appActivation = _db.UserActivation.FirstOrDefault(activation => activation.UserId == userid);
            //user.Activation.Add(new UserActivation
            //{
            //    UserId = Convert.ToInt64(row["userid"]),
            //    ActivationId = Convert.ToInt64(row["activationid"]),
            //    ActivationDate = Convert.ToString(row["activedate"]),
            //    ActivationToken = Convert.ToString(row["token"]),
            //    IsActive = Convert.ToBoolean(row["active"]),
            //    TokenType = Convert.ToString(row["tokentype"]),
            //});
            return loginUser;
        }

        public void SaveActivation(UserActivation activation)
        {
            throw new NotImplementedException();
        }

        public void SaveUserToken(UserToken userTokens)
        {
            throw new NotImplementedException();
        }

        public async Task<User> SaveUser(AppUserMaster appUser)
        {
            User user = new User()
            {
                UserName = appUser.Email,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
            };

            _db.AppUserMaster.Add(appUser);
            await _db.SaveChangesAsync();
            return user;
        }
    }
}
