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
    public class UserActivation : UserBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ActivationId { get; set; }
        public bool IsActive { get; set; }
        public string ActivationDate { get; set; }
        public string ActivationToken { get; set; }
        public string TokenType { get; set; }

        public AppUserMaster AppUserMaster { get; set; }    
    }
}
