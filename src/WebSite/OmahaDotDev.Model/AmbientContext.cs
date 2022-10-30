namespace OmahaDotDev.Model
{
    public record AmbientContext
    {
        public int GroupId { get; init; }
        public string? UserId { get; init; }
        public bool IsLoggedIn { get; init; } = false;
    }
}
