using MatchManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Domain.Entities.Account
{
    public class UserToken : UserBaseEntity
    {
        public int HashId { get; set; }
        public string PasswordSalt { get; set; }
    }
}
