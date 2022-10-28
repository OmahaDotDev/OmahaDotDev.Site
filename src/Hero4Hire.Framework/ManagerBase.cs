namespace Hero4Hire.Framework
{
    public abstract class ManagerBase<TContext> : ServiceBase<TContext>
    {
        public ManagerBase(TContext ambientContext, ServiceFactory<TContext> serviceFactory) : base(ambientContext, serviceFactory)
        {

        }
    }
}
