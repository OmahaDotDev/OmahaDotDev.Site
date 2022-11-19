using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OmahaDotDev.Model.Common;
using OmahaDotDev.ResourceAccess.Database;
using Respawn;
using Respawn.Graph;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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

        private Respawner _respawner;

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
                            services.ReplaceOrAddService<MockClaims>(provider => new MockClaims(CurrentClaims));
                            // .ReplaceOrAddService<AmbientContext>(provider => CurrentAmbientContext)


                        }
                    ));

            ScopeFactory = AppFactory.Services.GetRequiredService<IServiceScopeFactory>();

            //using var x = ScopeFactory.CreateScope();
            //var y = x.ServiceProvider.GetService<AmbientContext>();
            //var z = x.ServiceProvider.GetService<MockClaims>();


        }

        //Create a user
        public void RunAsUser(string userId)
        {

            CurrentAmbientContext = new AmbientContext() { IsLoggedIn = true, UserId = userId };

        }

        public async Task ResetSystemState()
        {
            CurrentAmbientContext = new AmbientContext() { UserId = null, IsLoggedIn = false };

            using var scope = ScopeFactory.CreateScope();
            await using var db = scope.ServiceProvider.GetRequiredService<SiteDbContext>();

            await _respawner.ResetAsync(db.Database.GetConnectionString());
        }


        #region Lifetime
        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public Task DisposeAsync()
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            using var scope = ScopeFactory.CreateScope();
            await using var db = scope.ServiceProvider.GetRequiredService<SiteDbContext>();

            _respawner = await Respawner.CreateAsync(db.Database.GetConnectionString(), new RespawnerOptions
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
