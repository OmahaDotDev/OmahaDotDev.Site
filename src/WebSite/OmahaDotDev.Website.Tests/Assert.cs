using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OmahaDotDev.ResourceAccess.Database;
using IdentityDbContext = OmahaDotDev.WebSite.Data.IdentityDbContext;

namespace OmahaDotDev.Website.Tests;

class Assert : IDisposable
{

    private readonly IServiceScope _scope;


    public SiteDbContext SiteDb { get; internal set; }
    public WebSite.Data.IdentityDbContext IdentityDb { get; internal set; }
    internal Assert(IServiceScopeFactory scopeFactory)
    {
        _scope = scopeFactory.CreateScope();
        IdentityDb = _scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
        SiteDb = _scope.ServiceProvider.GetRequiredService<SiteDbContext>();
    }


    public void Dispose()
    {
        _scope.Dispose();
    }
}