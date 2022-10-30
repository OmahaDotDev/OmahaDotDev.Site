namespace OmahaDotDev.ResourceAccess.Database.Entities
{
    internal class AuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedByUserId { get; set; } = null!;
        public string UpdatedByUserId { get; set; } = null!;
        public MemberRecord CreatedByUser { get; set; } = null!;
        public MemberRecord UpdatedByUser { get; set; } = null!;
    }
}
