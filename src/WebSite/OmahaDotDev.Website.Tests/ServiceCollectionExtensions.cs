using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaDotDev.Website.Tests
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ReplaceOrAddService<T>(this IServiceCollection serviceCollection,
            Func<IServiceProvider, object> factory)
        {
            ServiceDescriptor? currentUserServiceDescriptor = serviceCollection.FirstOrDefault(d =>
                d.ServiceType == typeof(T));

            if (currentUserServiceDescriptor != null)
            {
                serviceCollection.Remove(currentUserServiceDescriptor);
            }


            serviceCollection.Add(new ServiceDescriptor(typeof(T), factory,
                currentUserServiceDescriptor?.Lifetime ?? ServiceLifetime.Transient));

            return serviceCollection;
        }
    }
}
