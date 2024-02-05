using MatchManager.Domain.Common.Interface;

namespace MatchManager.Domain.Common
{
    public abstract class UserBaseEntity : IAuditableBaseEntity
    {
        public long UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
