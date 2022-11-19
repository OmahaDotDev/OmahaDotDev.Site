using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace OmahaDotDev.WebSite.Auth;

public class CustomAuthorizationRequirement : IAuthorizationRequirement
{

}
public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
{
    protected override Task
        HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
    {

        if (
            context.User.Identity is { IsAuthenticated: true } &&
            context.User.FindFirstValue(ClaimTypes.NameIdentifier)?.Length > 10
        )
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}