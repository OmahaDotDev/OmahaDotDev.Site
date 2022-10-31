namespace OmahaDotDev.Model.Accessors.Group
{
    public interface IGroupAdminResourceAccess
    {
        Task<GroupResponse> CreateGroup(CreateGroupRequest request, CancellationToken cancellationToken);
        Task<GroupResponse> UpdateGroup(UpdateGroupRequest request, CancellationToken cancellationToken);
        Task<GroupResponse> GetGroup(int id, CancellationToken cancellationToken);
        Task<IEnumerable<GroupResponse>> GetAllGroups(GetAllGroupsRequest request, CancellationToken cancellationToken);
        Task<GroupResponse> FindGroupByDomainName(string domainName, CancellationToken cancellationToken);
        Task SetGroupStatus(SetGroupStatusRequest request, CancellationToken cancellationToken);
    }
}
