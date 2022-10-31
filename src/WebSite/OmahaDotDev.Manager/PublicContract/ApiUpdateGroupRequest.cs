namespace OmahaDotDev.Manager.PublicContract
{
    public record ApiUpdateGroupRequest(string Name, string DomainName)
    {
        public int Id { get; init; }
    }
}
