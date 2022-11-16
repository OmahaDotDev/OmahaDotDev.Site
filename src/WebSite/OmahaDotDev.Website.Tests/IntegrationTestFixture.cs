using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using OmahaDotDev.Model.Common;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Xunit;

namespace OmahaDotDev.Website.Tests
{
    //https://github.com/raw-coding-youtube/testing-101/blob/main/RawCoding.WebApp/RawCoding.WebApp.IntegrationTests/AppInstance.cs
    //https://www.youtube.com/watch?v=b1-KG_x-Y5Q

    public class IntegrationTestFixture : IDisposable, IAsyncLifetime
    {
        public AmbientContext CurrentAmbientContext { get; set; } = new AmbientContext() { IsLoggedIn = false };
        public WebApplicationFactory<WebSite.Program> AppFactory { get; set; }
        public IServiceScopeFactory ScopeFactory { get; set; }

        public IntegrationTestFixture()
        {
            AppFactory = new WebApplicationFactory<WebSite.Program>()
                .WithWebHostBuilder(
                    builder => builder.ConfigureServices(
                        services => services
                            .ReplaceOrAddService<AmbientContext>(provider => CurrentAmbientContext)
                            .ReplaceOrAddService<MockClaimSeed>(provider => new MockClaimSeed(new List<Claim>()))
                    ));
                    // .
                    //          //   .WithAlternativeService(provider => CurrentAmbientContext)
                    //             .WithAlternativeService(provider => new MockClaimSeed(new List<Claim>()))
                ;

            ScopeFactory = AppFactory.Services.GetRequiredService<IServiceScopeFactory>();

            using var x = ScopeFactory.CreateScope();
            var y = x.ServiceProvider.GetService<AmbientContext>();
            var z = x.ServiceProvider.GetService<MockClaimSeed>();
        }

        //Create a user
        public async Task<string> RunAsUserAsync()
        {
            using var scope = ScopeFactory.CreateScope();
            using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();


            var newUser = new IdentityUser("Regular User");
            var result = await userManager.CreateAsync(newUser);

            CurrentAmbientContext = new AmbientContext() { IsLoggedIn = true, UserId = newUser.Id };
            return newUser.Id;

        }

        public async Task<string> RunAsSiteAdminAsync()
        {
            using var scope = ScopeFactory.CreateScope();
            using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();


            var newUser = new IdentityUser("Site Admin");
            var result = await userManager.CreateAsync(newUser);

            CurrentAmbientContext = new AmbientContext() { IsLoggedIn = true, UserId = newUser.Id };
            return newUser.Id;
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

        public Task InitializeAsync()
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
        #endregion
    }
}
