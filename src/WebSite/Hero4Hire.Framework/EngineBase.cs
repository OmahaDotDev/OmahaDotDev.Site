namespace Hero4Hire.Framework
{
    public abstract class EngineBase<TContext> : ServiceBase<TContext>
    {
        public EngineBase(TContext ambientContext, ServiceFactory<TContext> serviceFactory) : base(ambientContext, serviceFactory)
        {

        }
    }
}
