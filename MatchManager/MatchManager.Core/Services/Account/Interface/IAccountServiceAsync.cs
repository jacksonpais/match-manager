using MatchManager.Core.Wrappers;
using MatchManager.DTO.Account;

namespace MatchManager.Core.Services.Account.Interface
{
    public interface IAccountServiceAsync
    {
        bool IsUserPresent(string username);

        Task<CoreResult> Register(RegisterRequestDTO request);

        Task<CoreResult> Login(LoginRequestDTO request);
    }
}
