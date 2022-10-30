using Microsoft.Extensions.DependencyInjection;


namespace Hero4Hire.TimeUtility
{
    public static class Startup
    {
        public static IServiceCollection AddTimeUtility(this IServiceCollection services)
        {
            services.AddScoped<ITimeUtility>(provider => provider.GetRequiredService<TimeUtility>());
            return services;
        }
    }
}