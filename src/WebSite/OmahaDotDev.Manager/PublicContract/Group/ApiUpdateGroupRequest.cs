namespace OmahaDotDev.Manager.PublicContract.Group
{
    public record ApiUpdateGroupRequest(string Name, string DomainName)
    {
        public int Id { get; init; }
    }
}
