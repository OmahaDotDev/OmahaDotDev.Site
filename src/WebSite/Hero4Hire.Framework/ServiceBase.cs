namespace Hero4Hire.Framework
{
    public abstract class ServiceBase<TContext>
    {

        public TContext AmbientContext { get; internal set; }

        public ServiceFactory<TContext> ServiceFactory { get; internal set; }

    }
}
