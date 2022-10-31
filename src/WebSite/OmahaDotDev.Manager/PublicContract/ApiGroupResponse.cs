namespace OmahaDotDev.Manager.PublicContract
{
    public record ApiGroupResponse(string Name, string Url)
    {
        public int Id { get; set; }
    }
}
