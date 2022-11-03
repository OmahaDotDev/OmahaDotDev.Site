namespace OmahaDotDev.Manager.PublicContract
{
    public record ApiCreateGroupRequest(string Name, IEnumerable<string> DomainNames)
    {

    }
}
