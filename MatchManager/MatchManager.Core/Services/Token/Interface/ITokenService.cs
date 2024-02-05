using MatchManager.Domain.Entities.User;

namespace MatchManager.Core.Services.Token.Interface
{
    public interface ITokenService
    {
        string CreateToken(LoginUser user);
    }
}
