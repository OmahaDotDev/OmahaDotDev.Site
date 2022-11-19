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
        //public List<Claim> CurrentClaims { get  {
        //        if (CurrentAmbientContext.IsLoggedIn)
        //        {
        //            return new List<Claim>() { new Claim(ClaimTypes.NameIdentifier,
        //                CurrentAmbientContext.UserId)};
        //        }
        //        else
        //        {
        //            return new List<Claim>(); 
        //        }

        //    } }

        public List<Claim> CurrentClaims { get; set; } = new List<Claim>()  { new Claim(ClaimTypes.NameIdentifier,
                        "wut wut")};
        public WebApplicationFactory<WebSite.Program> AppFactory { get; set; }
        public IServiceScopeFactory ScopeFactory { get; set; }

        public IntegrationTestFixture()
        {
            AppFactory = new WebApplicationFactory<WebSite.Program>()
                .WithWebHostBuilder(
                    builder => builder.ConfigureServices(
                        services => {
                            services.AddSingleton<IAuthenticationSchemeProvider, MockSchemeProvider>();
                            services.ReplaceOrAddService<MockClaims>(provider => new MockClaims(CurrentClaims));
                               // .ReplaceOrAddService<AmbientContext>(provider => CurrentAmbientContext)
                                
                                
                            }
                    ));

            ScopeFactory = AppFactory.Services.GetRequiredService<IServiceScopeFactory>();

            using var x = ScopeFactory.CreateScope();
            var y = x.ServiceProvider.GetService<AmbientContext>();
            var z = x.ServiceProvider.GetService<MockClaims>();
        }

        //Create a user
        public void RunAsUser(string userId)
        {
            

            CurrentAmbientContext = new AmbientContext() { IsLoggedIn = true, UserId = userId };
            if (CurrentAmbientContext.IsLoggedIn)
            {
                CurrentClaims =  new List<Claim>() { new Claim(ClaimTypes.NameIdentifier,
                        CurrentAmbientContext.UserId)};
            }
            else
            {
                CurrentClaims = new List<Claim>();
            }

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
