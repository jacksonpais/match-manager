namespace MatchManager.Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
