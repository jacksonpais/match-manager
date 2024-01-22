using MatchManager.Core.Services.Account.Interface;
using MatchManager.DTO.Account;
using MatchManager.Infrastructure.Repositories.Account.Interface;

namespace MatchManager.Core.Services.Account
{
    public class AccountServiceAsync : IAccountServiceAsync
    {
        private readonly IAccountRepositoryAsync _accountRepository;

        public AccountServiceAsync(IAccountRepositoryAsync accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public bool IsUserPresent(string username)
        {
            return _accountRepository.IsUserPresent(username);
        }

        public Task<UserDTO> Login(LoginDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> Register(RegisterDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
