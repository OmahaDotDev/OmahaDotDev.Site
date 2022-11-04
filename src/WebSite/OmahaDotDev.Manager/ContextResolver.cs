using Hero4Hire.Framework;
using OmahaDotDev.Model.Common;

namespace OmahaDotDev.Manager
{


    class ContextResolver : IContextResolver<AmbientContext>
    {
        public AmbientContext GetContext()
        {
            return new AmbientContext()
            {
                GroupId = 1,
                IsLoggedIn = true,
                UserId = "1"
            };
        }
    }
}
