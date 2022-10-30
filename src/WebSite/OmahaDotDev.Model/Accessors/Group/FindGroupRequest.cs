namespace OmahaDotDev.Model.Accessors.Group
{
    public record FindGroupRequest(string Search, int Skip, int Take = 10, bool IncludeDeleted = false)
    {
    }
}
