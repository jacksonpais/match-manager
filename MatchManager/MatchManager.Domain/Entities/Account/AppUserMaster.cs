using MatchManager.Domain.Common;

namespace MatchManager.Domain.Entities.Account
{
    public class AppUserMaster : UserBaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Initial { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public string? BirthDate { get; set; }
        public int GenderId { get; set; }
        public string? Description { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsFirstTimeLoggedInUser { get; set; }
    }
}