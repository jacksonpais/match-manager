using AutoMapper;
using MatchManager.Data.Context;
using MatchManager.Domain.Entities.Account;
using MatchManager.Domain.Entities.User;
using MatchManager.Domain.Enums;
using MatchManager.Infrastructure.Repositories.Account.Interface;
using Microsoft.EntityFrameworkCore;
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

        public async Task<AppUserMaster> GetUser(long userid)
        {
            return await _db.AppUserMaster.FirstOrDefaultAsync(user => user.UserId == userid);
        }

        public async Task<AppUserMaster> GetUser(string username)
        {
            return await _db.AppUserMaster.FirstOrDefaultAsync(user => user.Email.ToLower() == username.ToLower());
        }

        public async Task<UserActivation> GetUserActivation(long userid, ActivationType activationType)
        {
            return await _db.UserActivation.FirstOrDefaultAsync(activation => activation.UserId == userid && activationType == activationType);
        }

        public async Task<List<UserActivation>> GetUserActivations(long userid)
        {
            return await _db.UserActivation.Where(activation => activation.UserId == userid).ToListAsync();
        }

        public async Task<long> GetUserId(string username)
        {
            long userid = 0;
            var user = await GetUser(username);
            if (user != null)
            {
                userid = user.UserId;
            }
            return userid;
        }

        public async Task<string> GetUserSaltbyUserid(long userid)
        {
            UserToken userToken = await GetUserToken(userid);
            return userToken.PasswordSalt;
        }

        public async Task<UserToken> GetUserToken(long userid)
        {
            var userToken = await _db.UserToken.FirstOrDefaultAsync(user => user.UserId == userid);
            return userToken;
        }

        public async Task<bool> IsUserPresent(string username)
        {
            bool rtn = false;
            var userid = await GetUserId(username);
            if (userid != 0)
            {
                rtn = true;
            }
            return rtn;
        }

        public async Task<LoginUser> LoginUser(long userid)
        {
            if (userid != 0)
            {
                AppUserMaster user = await GetUser(userid);
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

        public void SaveUserActivation(UserActivation activation)
        {
            _db.UserActivation.Update(activation);
        }

        public void SaveUserToken(UserToken userTokens)
        {
            _db.UserToken.Add(userTokens);
        }
    }
}
