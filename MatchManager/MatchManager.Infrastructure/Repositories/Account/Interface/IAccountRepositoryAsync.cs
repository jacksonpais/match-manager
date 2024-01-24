using MatchManager.Domain.Entities.Account;
using MatchManager.Domain.Entities.User;
using MatchManager.Domain.Enums;

namespace MatchManager.Infrastructure.Repositories.Account.Interface
{
    public interface IAccountRepositoryAsync
    {
        LoginUser LoginUser(long userid);
        long GetUserId(string email);
        bool IsUserPresent(string email);
        Task<User> SaveUser(AppUserMaster user);
        AppUserMaster GetUser(long userid);
        void SaveUserToken(UserToken userTokens);
        UserActivation GetActivation(LoginUser user, ActivationType activationType);
        void SaveActivation(UserActivation activation);
        bool CheckIfEmailActivated(long userid);
        string GetUserSaltbyUserid(LoginUser user);
    }
}