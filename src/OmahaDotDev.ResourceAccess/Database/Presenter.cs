namespace OmahaDotDev.ResourceAccess.Database
{
    internal class Presenter : AuditableEntity
    {
        public Presenter()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? ContactInfo { get; set; }

        public string? OmahaMtgUserId { get; set; }
        public OmahaMtgUser? User { get; set; }
        //public ICollection<PresentationPresenter> PresentationPresenters { get; set; }
        public bool IsDeleted { get; set; }
    }
}
