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

        long GetUserId(string username);
        AppUserMaster GetUser(long userid);
        AppUserMaster GetUser(string username);
        UserActivation GetUserActivation(long userid, ActivationType activationType);
        List<UserActivation> GetUserActivations(long userid);
        UserToken GetUserToken(long userid);

        LoginUser LoginUser(long userid);      
        bool IsUserPresent(string username);   
        
        string GetUserSaltbyUserid(long userid);
    }
}