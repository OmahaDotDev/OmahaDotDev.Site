namespace OmahaDotDev.ResourceAccess.Database.Model
{
    class MemberRecord
    {
        public MemberRecord(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
        public IEnumerable<GroupRecord> CreatedGroups { get; set; } = new List<GroupRecord>();

        public IEnumerable<GroupRecord> UpdatedGroups { get; set; } = new List<GroupRecord>();

        public IEnumerable<GroupDomainNameRecord> CreatedGroupDomainNames { get; set; } = new List<GroupDomainNameRecord>();

        public IEnumerable<GroupDomainNameRecord> UpdatedGroupDomainNames { get; set; } = new List<GroupDomainNameRecord>();
    }
}
