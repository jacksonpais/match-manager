using System.ComponentModel.DataAnnotations;

namespace MatchManager.DTO.Account
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Enter your email")]
        [EmailAddress(ErrorMessage = "Enter a valid e-mail address")]
        [MaxLength(100, ErrorMessage = "Email can only be maximum of 100 characters")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [MinLength(8, ErrorMessage = "Minimum Password must be 8 in characters")]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Your password must be at least 8 characters long and contain at least 1 letter and 1 number")]
        public required string Password { get; set; }
    }
}