using MatchManager.DTO.Account;

namespace MatchManager.Core.Services.Account.Interface
{
    public interface IAccountServiceAsync
    {
        bool IsUserPresent(string username);

        Task<UserDTO> Register(RegisterDTO request);

        Task<UserDTO> Login(LoginDTO request);
    }
}
