using Hero4Hire.Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using OmahaDotDev.Manager.PublicContract;
using OmahaDotDev.Model.Accessors.Group;
using OmahaDotDev.Model.Common;

namespace OmahaDotDev.Manager
{
    public static class GroupManagerStartup
    {
        public static IEndpointRouteBuilder MapGroupManagerRoutes(this IEndpointRouteBuilder app)
        {
            app.MapPost("/Groups",
                async (ApiCreateGroupRequest request, IGroupManager manager, CancellationToken token) =>
                await manager.CreateGroup(request, token));
            return app;
        }
    }


    internal class GroupManager : ManagerBase<AmbientContext>, IGroupManager
    {
        private readonly IGroupAdminResourceAccess _groupAdminResourceAccess;
        public GroupManager(AmbientContext ambientContext, ServiceFactory<AmbientContext> serviceFactory)
            : base(ambientContext, serviceFactory)
        {
            _groupAdminResourceAccess = serviceFactory.CreateService<IGroupAdminResourceAccess>();
        }

        public async Task<ApiGroupResponse> CreateGroup(ApiCreateGroupRequest request, CancellationToken cancellationToken)
        {
            var accessorRequest = new CreateGroupRequest(request.Name, request.DomainNames);
            var accessorResult = await _groupAdminResourceAccess.CreateGroup(accessorRequest, cancellationToken);
            var apiResult = new ApiGroupResponse(accessorResult.Name, accessorResult.DomainNames)
            {
                Id = accessorResult.Id,
            };

            return apiResult;
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
