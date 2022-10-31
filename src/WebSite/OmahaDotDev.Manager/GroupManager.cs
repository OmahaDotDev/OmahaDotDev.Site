using Hero4Hire.Framework;
using OmahaDotDev.Manager.PublicContract;
using OmahaDotDev.Model.Accessors.Group;
using OmahaDotDev.Model.Common;

namespace OmahaDotDev.Manager
{
    public class GroupManager : ManagerBase<AmbientContext>, IGroupManager
    {
        private readonly IGroupAdminResourceAccess _groupAdminResourceAccess;
        public GroupManager(AmbientContext ambientContext, ServiceFactory<AmbientContext> serviceFactory)
            : base(ambientContext, serviceFactory)
        {
            _groupAdminResourceAccess = serviceFactory.CreateService<IGroupAdminResourceAccess>();
        }

        public async Task<ApiGroupResponse> CreateGroup(ApiCreateGroupRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            //var accessRequest = new CreateGroupRequest(request.Name, request.DomainName);

            //var accessResult = await _groupAdminResourceAccess.CreateGroup(accessRequest, cancellationToken);

            //return new ApiGroupResponse(accessResult.Name, accessResult.DomainNames)
            //{
            //    Id = accessResult.Id,
            //};
        }

        public Task DeleteGroup(ApiDeleteGroupRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<SkipTakeSet<ApiGroupResponse>> FindAllGroups(ApiFindGroupRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApiGroupResponse> GetGroup(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApiGroupResponse> UpdateGroup(ApiUpdateGroupRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
