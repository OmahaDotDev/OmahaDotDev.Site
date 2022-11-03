namespace OmahaDotDev.Manager.PublicContract
{
    public record ApiGroupResponse(string Name, IEnumerable<string> DomainNames)
    {
        public int Id { get; set; }
    }
}
