using MatchManager.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Domain.Entities.User
{
    public class LoginUser
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string BirthDate { get; set; }
        public string PasswordHash { get; set; }
        public int GenderId { get; set; }
        public string Description { get; set; }
        public bool IsFirstTimeLoggedInUser { get; set; }
        public UserToken UserToken { get; set; }
        public List<UserActivation> Activation { get; set; }
    }
}
