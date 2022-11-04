
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OmahaDotDev.Model.Accessors.Group;
using OmahaDotDev.Model.Common;
using OmahaDotDev.ResourceAccess.Database;

namespace OmahaDotDev.ResourceAccess
{
    public static class Startup
    {
        public static IServiceCollection AddResourceAccess(this IServiceCollection services,
            SiteConfiguration configuration)
        {
            services.AddDbContext<SiteDbContext>(options =>
                options.UseSqlServer(
                    configuration.dbConnectionString,
                    b => b.MigrationsAssembly(typeof(SiteDbContext).Assembly.FullName)));

            services.AddScoped<IGroupAdminResourceAccess, GroupAccess>();


            return services;
        }
    }
}