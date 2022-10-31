using Hero4Hire.TimeUtility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OmahaDotDev.Model.Common;
using OmahaDotDev.ResourceAccess;

namespace OmahaDotDev.Manager
{
    public static class Startup
    {
        public static IServiceCollection AddManager(this IServiceCollection services, SiteConfiguration siteConfiguration)
        {
            services.AddResourceAccess(siteConfiguration);
            services.AddTimeUtility();
            return services;
        }

        public static IEndpointRouteBuilder MapManager(this IEndpointRouteBuilder app)
        {
            app.MapGet("/", () => "Hello World!");
            return app;
        }

    }


}