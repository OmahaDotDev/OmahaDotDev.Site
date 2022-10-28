namespace Hero4Hire.Framework
{
    public abstract class UtilityBase<TContext> : ServiceBase<TContext>
    {
        public UtilityBase(TContext ambientContext, ServiceFactory<TContext> serviceFactory) : base(ambientContext, serviceFactory)
        {

        }
    }
}
