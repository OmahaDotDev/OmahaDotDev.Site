namespace OmahaDotDev.ResourceAccess.Database
{
    internal class Meeting : AuditableEntity
    {
        public Meeting()
        {
            MeetingTags = new List<MeetingTag>();
            MeetingRsvps = new List<MeetingRsvp>();
            Presentations = new List<Presentation>(); ;
            MeetingSponsors = new List<MeetingSponsor>(); ;
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? PublishStartTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsDraft { get; set; }
        public IEnumerable<MeetingTag> MeetingTags { get; set; }
        public string? VideoId { get; set; }
        public int MaxRsvp { get; set; }
        public Supporter? MeetingHostSupporter { get; set; }
        public ICollection<MeetingRsvp> MeetingRsvps { get; set; }
        public int? MeetingHostSupporterId { get; set; }
        public string? HostMeetingBody { get; set; }
        public string? Intro { get; set; }
        public string? Footer { get; set; }
        public bool IsDeleted { get; set; }
        public int? OldMeetingId { get; set; }
        public IEnumerable<Presentation> Presentations { get; set; }
        public IEnumerable<MeetingSponsor> MeetingSponsors { get; set; }
    }
}
