using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OmahaDotDev.WebSite.Data;
using Respawn;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = true)]



namespace OmahaDotDev.Manager.Tests
{
    public class IntegrationTestFixture : IDisposable, IAsyncLifetime
    {
        public WebSiteApplicationFactory WebSiteApplicationFactory { get; init; }
        private readonly ApplicationDbContext _appDb;
        private readonly IServiceScope _scope;
        private readonly string _dbConnectionString;
        private Respawner _respawner = null!;

        public IntegrationTestFixture()
        {
            WebSiteApplicationFactory = new WebSiteApplicationFactory();
            _scope = WebSiteApplicationFactory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _appDb = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _dbConnectionString = _appDb.Database.GetDbConnection().ConnectionString;
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
            await _appDb.Database.EnsureDeletedAsync();
            await _appDb.Database.EnsureCreatedAsync();
            _respawner = await Respawner.CreateAsync(_dbConnectionString, new RespawnerOptions
            {

            });
        }
    }
}
