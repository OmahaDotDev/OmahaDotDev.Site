namespace OmahaDotDev.Model.Accessors.Group
{
    public record UpdateGroupRequest(string Name, string DomainName)
    {
        public int Id { get; init; }
    }
}
