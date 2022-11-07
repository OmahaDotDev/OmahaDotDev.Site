﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace OmahaDotDev.Manager.Tests
{
    public class WebSiteApplicationFactory : WebApplicationFactory<WebSite.Program>
    {
        private readonly string _environment = "Development";

        public WebSiteApplicationFactory()
        {

        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(_environment);

            // Add mock/test services to the builder here
            builder.ConfigureServices(services =>
            {
                //services.AddScoped(sp =>
                //{
                //    // Replace SQLite with in-memory database for tests
                //    return new DbContextOptionsBuilder<TodoDb>()
                //    .UseInMemoryDatabase("Tests")
                //    .UseApplicationServiceProvider(sp)
                //    .Options;
                //});
                //var config = new Model.Common.SiteConfiguration("Server=(localdb)\\mssqllocaldb;Database=aspnet-OmahaDotDev.WebSite-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true");
                //services.AddManager(config);
                //services.AddResourceAccess(config);
            });



            return base.CreateHost(builder);
        }





    }
}