
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OmahaDotDev.Model;
using OmahaDotDev.Model.Accessors.Group;
using OmahaDotDev.ResourceAccess.Database;

namespace OmahaDotDev.ResourceAccess
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            SiteConfiguration configuration)
        {
            services.AddDbContext<SiteDbContext>(options =>
                options.UseSqlServer(
                    configuration.dbConnectionString,
                    b => b.MigrationsAssembly(typeof(SiteDbContext).Assembly.FullName)));

            services.AddScoped<IGroupAdminResourceAccess>(provider => provider.GetRequiredService<GroupAccess>());


            return services;
        }
    }
}