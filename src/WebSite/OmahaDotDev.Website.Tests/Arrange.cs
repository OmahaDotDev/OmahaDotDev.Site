using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OmahaDotDev.Model.Common;
using OmahaDotDev.ResourceAccess.Database;
using OmahaDotDev.WebSite.Data;
using System.Runtime.CompilerServices;

namespace OmahaDotDev.Website.Tests;

public class Arrange : IDisposable
{
    private readonly IServiceScope _scope;
    private readonly IdentityDbContext _identityDb;
    private readonly SiteDbContext _siteDb;
    internal Arrange(IServiceScopeFactory scopeFactory)
    {
        _scope = scopeFactory.CreateScope();
        _identityDb = _scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
        _siteDb = _scope.ServiceProvider.GetRequiredService<SiteDbContext>();
    }


    public void Dispose()
    {
        _scope.Dispose();
    }

    public Task CreateTestUserAsync()
    {
        return Task.CompletedTask;
    }

    public async Task<string> CreateTestSiteAdminAsync()
    {
        using var userManager = _scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var newUser = new IdentityUser("SiteAdmin");
        var result = await userManager.CreateAsync(newUser);

        return newUser.Id;
    }

    public Task CreateTestGroupAdminAsync()
    {
        return Task.CompletedTask;

    }
}