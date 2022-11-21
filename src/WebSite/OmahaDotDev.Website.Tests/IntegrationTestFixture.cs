using Hero4Hire.TimeUtility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OmahaDotDev.Model.Common;
using OmahaDotDev.ResourceAccess.Database;
using OmahaDotDev.Website.Tests.Mocks;
using OmahaDotDev.WebSite.Data;
using Respawn;
using Respawn.Graph;
using System.Security.Claims;
using Xunit;

namespace OmahaDotDev.Website.Tests
{
    //https://github.com/raw-coding-youtube/testing-101/blob/main/RawCoding.WebApp/RawCoding.WebApp.IntegrationTests/AppInstance.cs
    //https://www.youtube.com/watch?v=b1-KG_x-Y5Q

    public class IntegrationTestFixture : IDisposable, IAsyncLifetime
    {
        public AmbientContext CurrentAmbientContext { get; set; } = new AmbientContext() { IsLoggedIn = false };
        public List<Claim> CurrentClaims
        {
            get
            {
                if (CurrentAmbientContext.IsLoggedIn && CurrentAmbientContext.UserId != null)
                {
                    return new List<Claim>() { new Claim(ClaimTypes.NameIdentifier,
                        CurrentAmbientContext.UserId)};
                }
                else
                {
                    return new List<Claim>();
                }

            }
        }

        private Respawner? _respawner;
        public readonly MockTimeUtility TimeUtility = new MockTimeUtility();
        public WebApplicationFactory<WebSite.Program> AppFactory { get; set; }
        public IServiceScopeFactory ScopeFactory { get; set; }

        public IntegrationTestFixture()
        {
            AppFactory = new WebApplicationFactory<WebSite.Program>()
                .WithWebHostBuilder(
                    builder => builder.ConfigureServices(
                        services =>
                        {
                            services.AddSingleton<IAuthenticationSchemeProvider, MockSchemeProvider>();
                            services.ReplaceOrAddService<MockClaims>(_ => new MockClaims(CurrentClaims));
                            services.ReplaceOrAddService<ITimeUtility>(_ => TimeUtility);
                        }
                    ));

            ScopeFactory = AppFactory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        public void RunAsUser(string userId)
        {
            CurrentAmbientContext = new AmbientContext() { IsLoggedIn = true, UserId = userId };
        }

        public async Task ResetSystemState()
        {
            CurrentAmbientContext = new AmbientContext() { UserId = null, IsLoggedIn = false };

            using var scope = ScopeFactory.CreateScope();
            await using var db = scope.ServiceProvider.GetRequiredService<SiteDbContext>();

            await _respawner!.ResetAsync(db.Database.GetConnectionString()!);
        }


        #region Lifetime
        public void Dispose()
        {
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            using var scope = ScopeFactory.CreateScope();
            await using var db = scope.ServiceProvider.GetRequiredService<SiteDbContext>();
            await using var identityDb = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

            await identityDb.Database.EnsureDeletedAsync();
            await identityDb.Database.MigrateAsync();
            await db.Database.MigrateAsync();

            _respawner = await Respawner.CreateAsync(db.Database.GetConnectionString()!, new RespawnerOptions
            {
                TablesToIgnore = new Table[]
                {
                    "__EFMigrationsHistory"
                },

            });
        }
        #endregion
    }
}
