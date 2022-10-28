namespace OmahaDotDev.ResourceAccess.Database
{
    internal class Post : AuditableEntity
    {
        public Post()
        {

            PostTags = new List<PostTag>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? PublishStartTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDraft { get; set; }
        public IEnumerable<PostTag> PostTags { get; set; }
    }
}
