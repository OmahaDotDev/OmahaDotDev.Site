namespace Hero4Hire.Framework
{
    public abstract class AccessorBase<TContext> : ServiceBase<TContext>
    {
        public AccessorBase(TContext ambientContext, ServiceFactory<TContext> serviceFactory) : base(ambientContext, serviceFactory)
        {

        }
    }
}
