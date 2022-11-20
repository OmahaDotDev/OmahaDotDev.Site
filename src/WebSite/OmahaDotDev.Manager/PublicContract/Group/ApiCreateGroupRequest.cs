namespace OmahaDotDev.Manager.PublicContract.Group
{
    public record ApiCreateGroupRequest(string Name, IEnumerable<string> DomainNames)
    {

    }
}
