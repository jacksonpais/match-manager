using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Domain.Entities.Account
{
    public class UserActivation
    {
        public bool IsActive { get; set; }
        public string ActivationDate { get; set; }
        public string ActivationToken { get; set; }
        public long UserId { get; set; }
        public long ActivationId { get; set; }
        public string TokenType { get; set; }
    }
}
