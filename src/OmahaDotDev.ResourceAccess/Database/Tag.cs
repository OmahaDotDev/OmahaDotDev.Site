namespace OmahaDotDev.ResourceAccess.Database
{
    internal class Tag : AuditableEntity
    {
        public Tag()
        {

            PostTags = new List<PostTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PostTag> PostTags { get; set; }
    }
}
