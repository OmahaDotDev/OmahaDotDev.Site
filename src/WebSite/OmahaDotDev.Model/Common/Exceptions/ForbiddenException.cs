namespace OmahaDotDev.Model.Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(AmbientContext ambientContext, string action)
            : base($"User {ambientContext.UserId} does not have required permission to perform action: {action}")
        {
        }
    }
}
