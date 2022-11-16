using Microsoft.AspNetCore.Mvc.Testing;
using OmahaDotDev.Model.Common;

namespace OmahaDotDev.Website.Tests;

public static class WebApplicationFactoryExtensions
{
    public static WebApplicationFactory<TProgram> WithAlternativeService<TService, TProgram>(this WebApplicationFactory<TProgram> webApplicationFactory, Func<IServiceProvider, TService> factory) where TProgram : class where TService : class
    {
        return webApplicationFactory.WithWebHostBuilder(
            builder => builder.ConfigureServices(
                services => services.ReplaceOrAddService<AmbientContext>(factory)
            )
        ); ;

    }
}