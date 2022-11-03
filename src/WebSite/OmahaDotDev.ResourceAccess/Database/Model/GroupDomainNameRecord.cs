namespace OmahaDotDev.ResourceAccess.Database.Model
{
    class GroupDomainNameRecord : AuditableEntity
    {
        public GroupDomainNameRecord(string domainName)
        {
            DomainName = domainName;

        }

        public int Id { get; set; }
        public string DomainName { get; set; }
        public int GroupId { get; set; }

        private GroupRecord? _group;
        public GroupRecord Group
        {
            set => _group = value;
            get => _group ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Group));
        }
    }
}
