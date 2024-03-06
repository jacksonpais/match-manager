using MatchManager.Domain.Entities.Account;

namespace MatchManager.Domain.Entities.User
{
    public class LoginUser
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Initial { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string BirthDate { get; set; }
        public string PasswordHash { get; set; }
        public int GenderId { get; set; }
        public string Description { get; set; }
        public bool IsFirstTimeLoggedInUser { get; set; }
        public UserToken Token { get; set; }
        public List<UserActivation> Activations { get; set; }
    }
}
