namespace OmahaDotDev.ResourceAccess.Database
{
    internal class MeetingSponsor : AuditableEntity
    {
        public MeetingSponsor()
        {

        }
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public int? SupporterId { get; set; }
        public Supporter? Supporter { get; set; }

        public string? MeetingSponsorBody { get; set; }
    }
}
