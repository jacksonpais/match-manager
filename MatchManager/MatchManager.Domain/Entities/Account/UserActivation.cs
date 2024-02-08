using MatchManager.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatchManager.Domain.Entities.Account
{
    [ComplexType]
    public class UserActivation : UserBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ActivationId { get; set; }
        public required bool IsActive { get; set; }
        public required DateTime ActivationDate { get; set; }
        public required string ActivationToken { get; set; }
        public required string TokenType { get; set; }

        public AppUserMaster? UserMaster { get; set; }    
    }
}
