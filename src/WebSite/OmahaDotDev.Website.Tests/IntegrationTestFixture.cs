using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using OmahaDotDev.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OmahaDotDev.Website.Tests
{
    public class IntegrationTestFixture : IDisposable, IAsyncLifetime
    {
        public AmbientContext CurrentAmbientContext { get; set; } = new AmbientContext() { IsLoggedIn = false };
        public WebApplicationFactory<WebSite.Program> AppFactory { get; set; }
        public IServiceScopeFactory ScopeFactory { get; set; }

        public IntegrationTestFixture()
        {
            AppFactory = new WebApplicationFactory<WebSite.Program>()
                                .WithReplacedService(provider => CurrentAmbientContext);

            ScopeFactory = AppFactory.Services.GetRequiredService<IServiceScopeFactory>();
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
