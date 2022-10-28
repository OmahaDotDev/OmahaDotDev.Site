namespace OmahaDotDev.ResourceAccess.Database
{
    internal class AuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedByUserId { get; set; } = null!;
        public string UpdatedByUserId { get; set; } = null!;
        public OmahaMtgUser CreatedByUser { get; set; } = null!;
        public OmahaMtgUser UpdatedByUser { get; set; } = null!;
    }
}
