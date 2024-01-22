using MatchManager.Core.Enums;
using MatchManager.Domain.Entities.Account;

namespace MatchManager.Infrastructure.Repositories.Account.Interface
{
    public interface IUserRepositoryAsync
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