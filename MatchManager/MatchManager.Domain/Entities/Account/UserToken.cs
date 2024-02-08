using MatchManager.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MatchManager.Domain.Entities.Account
{
    [ComplexType]
    public class UserToken : UserBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TokenId { get; set; }
        public required int HashId { get; set; }
        public required string PasswordSalt { get; set; }

        public AppUserMaster? UserMaster { get; set; }
    }
}
