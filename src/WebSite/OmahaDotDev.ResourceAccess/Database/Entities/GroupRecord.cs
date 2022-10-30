namespace OmahaDotDev.ResourceAccess.Database.Entities
{
    internal class GroupRecord : AuditableEntity
    {
        public GroupRecord(string name, string domainName)
        {
            Name = name;
            DomainName = domainName;
        }
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Name { get; set; }
        public string DomainName { get; set; }
    }
}
