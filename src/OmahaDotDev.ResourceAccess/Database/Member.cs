namespace OmahaDotDev.ResourceAccess.Database
{
    internal class OmahaMtgUser : AuditableEntity
    {

        public IEnumerable<Meeting> CreatedMeetings { get; set; } = new List<Meeting>();

        public IEnumerable<Meeting> UpdatedMeetings { get; set; } = new List<Meeting>();

        public IEnumerable<MeetingRsvp> CreatedMeetingRsvps { get; set; } = null!;

        public IEnumerable<MeetingRsvp> UpdatedMeetingRsvps { get; set; } = null!;

        public IEnumerable<MeetingSponsor> CreatedMeetingSupporters { get; set; } = null!;

        public IEnumerable<MeetingSponsor> UpdatedMeetingSupporters { get; set; } = null!;

        public IEnumerable<Post> CreatedPosts { get; set; } = null!;

        public IEnumerable<Post> UpdatedPosts { get; set; } = null!;

        public IEnumerable<Supporter> CreatedSupporters { get; set; } = null!;

        public IEnumerable<Supporter> UpdatedSupporters { get; set; } = null!;

        public IEnumerable<Tag> CreatedTags { get; set; } = null!;

        public IEnumerable<Tag> UpdatedTags { get; set; } = null!;

        public IEnumerable<PresentationPresenter> CreatedPresentationPresenters { get; set; } = null!;

        public IEnumerable<PresentationPresenter> UpdatedPresentationPresenters { get; set; } = null!;

        public IEnumerable<PostTag> CreatedPostTags { get; set; } = null!;

        public IEnumerable<PostTag> UpdatedPostTags { get; set; } = null!;

        public IEnumerable<MeetingTag> CreatedMeetingTags { get; set; } = null!;

        public IEnumerable<MeetingTag> UpdatedMeetingTags { get; set; } = null!;

        public IEnumerable<Presentation> CreatedPresentations { get; set; } = new List<Presentation>();

        public IEnumerable<Presentation> UpdatedPresentations { get; set; } = new List<Presentation>();

        public IEnumerable<Presenter> CreatedPresenters { get; set; } = null!;

        public IEnumerable<Presenter> UpdatedPresenters { get; set; } = null!;

        public Presenter? Presenter { get; set; } = null!;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int EmailFailures { get; set; }
        public bool IsUnsubscribed { get; set; }

        public DateTime? LastLoginDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
