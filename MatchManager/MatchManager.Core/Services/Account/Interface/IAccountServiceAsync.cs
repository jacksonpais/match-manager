using MatchManager.Core.Wrappers;
using MatchManager.DTO.Account;
using Microsoft.Win32;

namespace MatchManager.Core.Services.Account.Interface
{
    public interface IAccountServiceAsync
    {
        Task<bool> IsUserPresent(string username);

        Task<CoreResult> Register(RegisterRequestDTO request);

        Task<CoreResult> Login(LoginRequestDTO request);

        Task<CoreResult> VerifyAccount(VerifyAccountDTO request);
        Task<CoreResult> RequestVerificationLink(RequestVericationLinkDTO request);
    }
}
