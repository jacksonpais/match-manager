using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Domain.Entities.Account
{
    public class UserToken
    {
        public long TokenId { get; set; }
        public int HashId { get; set; }
        public string PasswordSalt { get; set; }
        public long UserId { get; set; }
        public string TokenDate { get; set; }
    }
}
