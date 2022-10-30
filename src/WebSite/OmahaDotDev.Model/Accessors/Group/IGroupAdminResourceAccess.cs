using OmahaDotDev.Model.Common;

namespace OmahaDotDev.Model.Accessors.Group
{
    public interface IGroupAdminResourceAccess
    {
        Task<GroupResponse> CreateGroup(CreateGroupRequest request, CancellationToken cancellationToken);

        Task DeleteGroup(DeleteGroupRequest request, CancellationToken cancellationToken);

        Task<SkipTakeSet<GroupResponse>> FindAllGroups(FindGroupRequest request, CancellationToken cancellationToken);

        Task<GroupResponse> GetGroup(int id, CancellationToken cancellationToken);

        Task<GroupResponse> UpdateGroup(UpdateGroupRequest request, CancellationToken cancellationToken);
    }
}
