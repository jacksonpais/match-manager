using MatchManager.Core.Services.Account.Interface;
using MatchManager.DTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Core.Services.Account
{
    public class AccountServiceAsync : IAccountServiceAsync
    {
        public bool IsUserPresent(string username)
        {
            throw new NotImplementedException();
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
