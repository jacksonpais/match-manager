using MatchManager.Data.Context;
using MatchManager.Domain.Entities.Account;
using MatchManager.Domain.Entities.User;
using MatchManager.Domain.Enums;

namespace MatchManager.Infrastructure.Repositories.Account.Interface
{
    public interface IAccountRepositoryAsync
    {
        Task SaveChangesToDBAsync();
        void SaveUser(AppUserMaster user);
        void SaveUserToken(UserToken userTokens);
        void SaveUserActivation(List<UserActivation> activation);
        void SaveUserActivation(UserActivation activation);

        Task<long> GetUserId(string username);
        Task<AppUserMaster> GetUser(long userid);
        Task<AppUserMaster> GetUser(string username);
        Task<UserActivation> GetUserActivation(long userid, ActivationType activationType);
        Task<List<UserActivation>> GetUserActivations(long userid);
        Task<UserToken> GetUserToken(long userid);

        Task<LoginUser> LoginUser(long userid);      
        Task<bool> IsUserPresent(string username);   
        
        Task<string> GetUserSaltbyUserid(long userid);
    }
}