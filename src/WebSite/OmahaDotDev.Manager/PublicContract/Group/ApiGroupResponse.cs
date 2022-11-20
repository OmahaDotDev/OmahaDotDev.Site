namespace OmahaDotDev.Manager.PublicContract.Group
{
    public record ApiGroupResponse(string Name, IEnumerable<string> DomainNames)
    {
        public int Id { get; set; }
    }
}
