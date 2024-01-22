using System.ComponentModel.DataAnnotations;

namespace MatchManager.DTO.Account
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Enter your first name")]
        [MinLength(2, ErrorMessage = "First name must be atleast of two characters")]
        [RegularExpression(@"^(?=.{2,15}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$", ErrorMessage = "Enter a valid first name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Enter your last name")]
        [MinLength(2, ErrorMessage = "Last name must be atleast of two characters")]
        [RegularExpression(@"^(?=.{2,15}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$", ErrorMessage = "Enter a valid last name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Enter your email")]
        [EmailAddress(ErrorMessage = "Enter a valid e-mail address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [MinLength(7, ErrorMessage = "Minimum Password must be 8 in characters")]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Your password must be at least 7 characters long and contain at least 1 letter and 1 number")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Enter Valid Password")]
        public string? ConfirmPassword { get; set; }
    }
}