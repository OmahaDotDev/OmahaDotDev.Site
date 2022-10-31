namespace OmahaDotDev.Manager.PublicContract
{
    public record ApiFindGroupRequest(string Search, int Skip, int Take = 10, bool IncludeDeleted = false)
    {
    }
}
