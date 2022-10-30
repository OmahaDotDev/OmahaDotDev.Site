namespace Hero4Hire.Framework
{
    public interface IContextResolver<out TContext>
    {
        TContext GetContext();
    }
}
