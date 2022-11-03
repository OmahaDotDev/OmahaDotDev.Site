namespace OmahaDotDev.ResourceAccess.Database.Model
{
    class GroupRecord : AuditableEntity
    {
        public GroupRecord(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public string Name { get; set; }
        public IEnumerable<GroupDomainNameRecord> DomainNames { get; set; } = new List<GroupDomainNameRecord>();
    }
}
