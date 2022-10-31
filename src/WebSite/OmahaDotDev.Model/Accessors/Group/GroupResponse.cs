namespace OmahaDotDev.Model.Accessors.Group
{
    public record GroupResponse(string Name, IEnumerable<string> DomainNames)
    {
        public int Id { get; set; }
    }
}
