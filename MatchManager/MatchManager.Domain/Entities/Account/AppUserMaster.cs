using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MatchManager.Domain.Common.Interface;

namespace MatchManager.Domain.Entities.Account
{
    public class AppUserMaster : IAuditableBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        [MaxLength(100)]
        public required string FirstName { get; set; }

        [MaxLength(100)]
        public required string LastName { get; set; }

        [MaxLength(100)]
        public required string UserName { get; set; }

        [MaxLength(2)]
        [NotMapped]
        public required string Initial { get; set; }

        [MaxLength(100)]
        public required string Email { get; set; }

        [MaxLength(100)]
        public required string MobileNo { get; set; }

        public DateTime BirthDate { get; set; }
        public required int GenderId { get; set; }
        public string? Description { get; set; }
        public required string PasswordHash { get; set; }
        public required bool IsFirstTimeLoggedInUser { get; set; }

        [ForeignKey("UserId")]
        public required UserToken UserToken { get; set; }

        [ForeignKey("UserId")]
        public required List<UserActivation> UserActivation { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}