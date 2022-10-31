namespace OmahaDotDev.Model.Accessors.Group
{
    public record UpdateGroupRequest(string Name, IEnumerable<string> DomainNames)
    {
        public int Id { get; init; }
    }
}
