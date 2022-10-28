namespace Hero4Hire.Framework
{
    public abstract class ServiceBase<TContext>
    {
        public ServiceBase(TContext ambientContext, ServiceFactory<TContext> serviceFactory)
        {
            AmbientContext = ambientContext;
            ServiceFactory = serviceFactory;
        }
        public TContext AmbientContext { get; internal set; }

        public ServiceFactory<TContext> ServiceFactory { get; internal set; }

    }
}
