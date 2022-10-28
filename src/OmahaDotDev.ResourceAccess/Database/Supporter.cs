namespace OmahaDotDev.ResourceAccess.Database
{
    internal class Supporter : AuditableEntity
    {
        public Supporter()
        {


        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanHost { get; set; }
        public bool CanSponsor { get; set; }
        public string? HostBlurb { get; set; }
        public string? BannerRotationBlurb { get; set; }
        public string? ContactInfo { get; set; }
        public string? SponsorUrl { get; set; }
        public bool IsDeleted { get; set; }
        public string? ShortHostBlurb { get; set; }

    }
}
