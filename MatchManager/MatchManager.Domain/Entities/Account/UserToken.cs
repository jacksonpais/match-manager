using MatchManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
