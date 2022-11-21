using Microsoft.EntityFrameworkCore;
using OmahaDotDev.Model.Common;
using OmahaDotDev.ResourceAccess.Database;

namespace OmahaDotDev.ResourceAccess;

static class DbHelpers
{
    public static async Task<Boolean> IsUserSiteAdmin(this SiteDbContext dbContext, AmbientContext ambientContext)
    {
        if (!ambientContext.IsLoggedIn)
            return false;

        var user = await dbContext.Members.FirstOrDefaultAsync(w => w.UserId == ambientContext.UserId);

        if (user == null) return false;

        return user.IsSiteAdmin;
    }

}