namespace OmahaDotDev.Model.Accessors.Group
{
    public record CreateGroupRequest(string Name, IEnumerable<string> DomainNames)
    {

    }
}
