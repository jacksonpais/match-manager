using System.ComponentModel.DataAnnotations;

namespace MatchManager.DTO.Account
{
    public class RequestVericationLinkDTO
    {
        [Required(ErrorMessage = "Enter your email")]
        [EmailAddress(ErrorMessage = "Enter a valid e-mail address")]
        [MaxLength(100, ErrorMessage = "Email can only be maximum of 100 characters")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Enter a valid verification type")]
        public required string VerificationType { get; set; }
    }
}