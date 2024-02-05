using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.DTO.Account
{
    public class VerifyAccountDTO
    {
        public string Key { get; set; }
        public string HashToken { get; set; }
    }
}
