namespace OmahaDotDev.Model.Accessors.Group
{
    public record GroupResponse(string Name, string Url)
    {
        public int Id { get; set; }
    }
}
