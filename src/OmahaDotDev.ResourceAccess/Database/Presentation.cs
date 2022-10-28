namespace OmahaDotDev.ResourceAccess.Database
{
    internal class Presentation : AuditableEntity
    {
        public Presentation()
        {

            PresentationPresenters = new List<PresentationPresenter>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Details { get; set; }
        public Meeting Meeting { get; set; }
        public int MeetingId { get; set; }
        public ICollection<PresentationPresenter> PresentationPresenters { get; set; }
        public bool IsDeleted { get; set; }
        public string? VimeoId { get; set; }
    }
}
