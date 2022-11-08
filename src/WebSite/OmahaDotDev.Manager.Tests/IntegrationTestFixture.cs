using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OmahaDotDev.ResourceAccess.Database;
using OmahaDotDev.ResourceAccess.Database.Model;
using OmahaDotDev.WebSite.Data;
using Respawn;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = true)]



namespace OmahaDotDev.Manager.Tests
{
    public class IntegrationTestFixture : IDisposable, IAsyncLifetime
    {
        public WebSiteApplicationFactory WebSiteApplicationFactory { get; init; }
        private readonly IdentityDbContext _identityDb;
        private readonly SiteDbContext _siteDb;
        private readonly IServiceScope _scope;
        private readonly string _dbConnectionString;
        private Respawner _respawner = null!;

        public IntegrationTestFixture()
        {
            WebSiteApplicationFactory = new WebSiteApplicationFactory();
            _scope = WebSiteApplicationFactory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _identityDb = _scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
            _siteDb = _scope.ServiceProvider.GetRequiredService<SiteDbContext>();
            _dbConnectionString = _siteDb.Database.GetDbConnection().ConnectionString;
        }
        public void Dispose()
        {
            _scope.Dispose();
            WebSiteApplicationFactory.Dispose();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }

        public async Task ResetDb()
        {
            await _respawner.ResetAsync(_dbConnectionString);
        }

        public async Task InitializeAsync()
        {
            //we are going to have to put the identity database in this thing as well :/
            await _identityDb.Database.EnsureDeletedAsync();
            await _siteDb.Database.EnsureDeletedAsync();
            await _siteDb.Database.MigrateAsync();
            await _identityDb.Database.MigrateAsync();

            _respawner = await Respawner.CreateAsync(_dbConnectionString, new RespawnerOptions
            {

            });
        }

        public async Task<string> CreateTestUser(string userName)
        {
            var _userManager = _scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var newUser = new IdentityUser(userName);
            var result = await _userManager.CreateAsync(newUser);
            _siteDb.Add(new MemberRecord(newUser.Id));
            return newUser.Id;
        }
    }
}
