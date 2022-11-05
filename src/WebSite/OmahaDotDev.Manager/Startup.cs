using Hero4Hire.Framework;
using Hero4Hire.TimeUtility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OmahaDotDev.Manager.PublicContract;
using OmahaDotDev.Model.Common;
using OmahaDotDev.ResourceAccess;

namespace OmahaDotDev.Manager
{


    public static class Startup
    {
        public static IServiceCollection AddManager(this IServiceCollection services, SiteConfiguration siteConfiguration)
        {
            services.AddTransient<IGroupManager, GroupManager>();
            services.AddTransient<IContextResolver<AmbientContext>, ContextResolver>();
            services.AddTransient<AmbientContext, AmbientContext>(sp =>
            {
                var x = sp.GetRequiredService<IContextResolver<AmbientContext>>();
                return x.GetContext();
            });


            services.AddTransient(typeof(ServiceFactory<AmbientContext>), typeof(ServiceFactory<AmbientContext>));

            services.AddResourceAccess(siteConfiguration);
            services.AddTimeUtility();

            return services;
        }

        public static IEndpointRouteBuilder MapManager(this IEndpointRouteBuilder app)
        {
            app.MapGet("/", () => "Hello Worldd!");
            app.MapGroupManagerRoutes();
            return app;
        }

    }


}