namespace MatchManager.Domain.Common.Interface
{
    public interface IAuditableBaseEntity
    {
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
