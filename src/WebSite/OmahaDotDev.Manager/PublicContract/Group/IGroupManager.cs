using OmahaDotDev.Model.Common;

namespace OmahaDotDev.Manager.PublicContract.Group
{
    public interface IGroupManager
    {
        Task<ApiGroupResponse> CreateGroup(ApiCreateGroupRequest request, CancellationToken cancellationToken);

        Task DeleteGroup(ApiDeleteGroupRequest request, CancellationToken cancellationToken);

        Task<SkipTakeSet<ApiGroupResponse>> FindAllGroups(ApiFindGroupRequest request, CancellationToken cancellationToken);

        Task<ApiGroupResponse> GetGroup(int id, CancellationToken cancellationToken);

        Task<ApiGroupResponse> UpdateGroup(ApiUpdateGroupRequest request, CancellationToken cancellationToken);
    }
}
