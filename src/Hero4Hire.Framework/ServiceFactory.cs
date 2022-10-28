
using Microsoft.Extensions.DependencyInjection;

namespace Hero4Hire.Framework
{
    public enum FactoryScope
    {
        Client,
        Manager,
        Accessor,
        Engine,
        Utility
    }
    public class ServiceFactory<TContext>
    {
        private readonly IContextResolver<TContext> _contextResolver;
        private readonly IServiceProvider _serviceProvider;
        private readonly FactoryScope _factoryScope;

        private readonly Dictionary<FactoryScope, List<FactoryScope>> _scopeMappings = new Dictionary<FactoryScope, List<FactoryScope>>()
        {
            // Client can only call Managers
            { FactoryScope.Client, new List<FactoryScope>() {FactoryScope.Manager}},

            //Manager can call Accessor, Engine, and Utility
            { FactoryScope.Manager, new List<FactoryScope>() {FactoryScope.Accessor, FactoryScope.Engine, FactoryScope.Utility}},

            //Engine can call Utility and Accessor
            { FactoryScope.Engine, new List<FactoryScope>() {FactoryScope.Accessor, FactoryScope.Utility}},

            //Accessor can call Utility
            { FactoryScope.Accessor, new List<FactoryScope>() {FactoryScope.Utility}},
        };

        public ServiceFactory(IServiceProvider serviceProvider, IContextResolver<TContext> contextResolver, FactoryScope factoryScope)
        {
            _serviceProvider = serviceProvider;
            _contextResolver = contextResolver;
            _factoryScope = factoryScope;
        }

        public ServiceFactory(IServiceProvider serviceProvider, IContextResolver<TContext> contextResolver)
        {
            _serviceProvider = serviceProvider;
            _contextResolver = contextResolver;
            _factoryScope = FactoryScope.Client;
        }

        private bool IsCallAllowedByIDesignRules<T>(T instance)
        {
            var allowedServices = _scopeMappings[_factoryScope];
            return instance switch
            {
                ManagerBase<TContext> @base => allowedServices.Contains(FactoryScope.Manager),
                AccessorBase<TContext> @base => allowedServices.Contains(FactoryScope.Accessor),
                UtilityBase<TContext> @base => allowedServices.Contains(FactoryScope.Utility),
                EngineBase<TContext> @base => allowedServices.Contains(FactoryScope.Engine),
                _ => false
            };
        }

        public T CreateService<T>() where T : class
        {
            T result = _serviceProvider.GetRequiredService<T>();

            if (!IsCallAllowedByIDesignRules(result))
            {
                throw new InvalidOperationException($"{typeof(T).Name} does not implement ManagerBase");
            }

            switch (result)
            {
                case ManagerBase<TContext> @base:
                    @base.AmbientContext = _contextResolver.GetContext();
                    @base.ServiceFactory = ActivatorUtilities.CreateInstance<ServiceFactory<TContext>>(_serviceProvider, FactoryScope.Manager);
                    break;
                case AccessorBase<TContext> @base:
                    @base.AmbientContext = _contextResolver.GetContext();
                    @base.ServiceFactory = ActivatorUtilities.CreateInstance<ServiceFactory<TContext>>(_serviceProvider, FactoryScope.Accessor);
                    break;
                case EngineBase<TContext> @base:
                    @base.AmbientContext = _contextResolver.GetContext();
                    @base.ServiceFactory = ActivatorUtilities.CreateInstance<ServiceFactory<TContext>>(_serviceProvider, FactoryScope.Engine);
                    break;
                case UtilityBase<TContext> @base:
                    @base.AmbientContext = _contextResolver.GetContext();
                    @base.ServiceFactory = ActivatorUtilities.CreateInstance<ServiceFactory<TContext>>(_serviceProvider, FactoryScope.Utility);
                    break;
                default:
                    // mocking of the manager factory is not supported so every result should implement ManagerBase
                    throw new InvalidOperationException($"{typeof(T).Name} does not implement ManagerBase");
            }

            return result;


        }
    }
}
