namespace OmahaDotDev.ResourceAccess.Database.Entities
{
    internal class GroupRecord : AuditableEntity
    {
        public GroupRecord(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public string Name { get; set; }
        public IEnumerable<GroupDomainName> DomainNames { get; set; } = new List<GroupDomainName>();
    }
}
