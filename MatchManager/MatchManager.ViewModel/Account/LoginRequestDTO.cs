using System.ComponentModel.DataAnnotations;

namespace MatchManager.DTO.Account
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Enter your email")]
        [EmailAddress(ErrorMessage = "Enter a valid e-mail address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [MinLength(7, ErrorMessage = "Minimum Password must be 8 in characters")]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Your password must be at least 7 characters long and contain at least 1 letter and 1 number")]
        public string? Password { get; set; }
    }
}