using MatchManager.Domain.Entities.Account;
using MatchManager.Domain.Enums;

namespace MatchManager.Infrastructure.Repositories.Account.Interface
{
    public interface IAccountRepositoryAsync
    {
        bool IsUserPresent(string email);
        void SaveUser(AppUserMaster user);
        AppUserMaster GetUser(long userid);
        AppUserMaster GetUser(string username);
        bool IsUserActivated(long userid);
        string GetUserSaltbyUserid(long userid);

        UserActivation GetActivation(AppUserMaster user, ActivationType activationType);
        void SaveActivation(UserActivation activation);

        void SaveUserToken(UserToken userTokens);
    }
}