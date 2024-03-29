using System.ComponentModel.DataAnnotations;

namespace MatchManager.DTO.Account
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Enter your first name")]
        [MinLength(2, ErrorMessage = "First name must be atleast of two characters")]
        [MaxLength(100, ErrorMessage = "First name can only be maximum of 100 characters")]
        [RegularExpression(@"^(?=.{2,25}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$", ErrorMessage = "Enter a valid first name")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Enter your last name")]
        [MinLength(2, ErrorMessage = "Last name must be atleast of two characters")]
        [MaxLength(100, ErrorMessage = "Last name can only be maximum of 100 characters")]
        [RegularExpression(@"^(?=.{2,25}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$", ErrorMessage = "Enter a valid last name")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Enter your email")]
        [EmailAddress(ErrorMessage = "Enter a valid e-mail address")]
        [MaxLength(100, ErrorMessage = "Email can only be maximum of 100 characters")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [MinLength(8, ErrorMessage = "Minimum Password must be 8 in characters")]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Your password must be at least 8 characters long and contain at least 1 letter and 1 number")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Enter Valid Password")]
        public required string ConfirmPassword { get; set; }
    }
}