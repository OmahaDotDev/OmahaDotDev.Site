namespace OmahaDotDev.ResourceAccess.Database
{
    internal class PostTag : AuditableEntity
    {
        private Post? _post;
        private Tag? _tag;

        public Tag Tag
        {
            set => _tag = value;
            get => _tag ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Tag));
        }

        public int TagId { get; set; }

        public Post Post
        {
            set => _post = value;
            get => _post ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Post));
        }

        public int PostId { get; set; }
    }
}
