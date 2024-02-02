using MatchManager.Domain.Entities.Account;
using MatchManager.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Core.Services.Token.Interface
{
    public interface ITokenService
    {
        string CreateToken(LoginUser user);
    }
}
