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
                async (ApiCreateGroupRequest request, ServiceFactory<AmbientContext> serviceFactory,
                    CancellationToken token) =>
                {
                    var manager = serviceFactory.CreateService<IGroupManager>();
                    return await manager.CreateGroup(request, token);
                });


            return app;
        }
    }


    internal class GroupManager : ManagerBase<AmbientContext>, IGroupManager
    {

        public async Task<ApiGroupResponse> CreateGroup(ApiCreateGroupRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var accessorRequest = new CreateGroupRequest(request.Name, request.DomainNames);
                var groupAdminResourceAccess = ServiceFactory.CreateService<IGroupAdminResourceAccess>();
                var accessorResult = await groupAdminResourceAccess.CreateGroup(accessorRequest, cancellationToken);
                var apiResult = new ApiGroupResponse(accessorResult.Name, accessorResult.DomainNames)
                {
                    Id = accessorResult.Id,
                };

                return apiResult;
            }
            catch (Exception ex)
            {
                throw;
            }
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
