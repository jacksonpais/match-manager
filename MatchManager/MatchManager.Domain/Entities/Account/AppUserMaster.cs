using MatchManager.Domain.Common;

namespace MatchManager.Domain.Entities.Account
{
    public class AppUserMaster : UserBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string PasswordHash { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}