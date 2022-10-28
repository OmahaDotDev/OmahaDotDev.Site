namespace OmahaDotDev.ResourceAccess.Database
{
    internal class MeetingTag : AuditableEntity
    {
        public MeetingTag()
        {

        }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}
