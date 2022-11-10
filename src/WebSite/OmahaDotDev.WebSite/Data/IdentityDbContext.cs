using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OmahaDotDev.Model.Common;

namespace OmahaDotDev.WebSite.Data
{
    public class IdentityDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  modelBuilder.HasDefaultSchema("identity");
            base.OnModelCreating(modelBuilder);
        }

        //    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        //CancellationToken cancellationToken = new CancellationToken())
        //    {



        //        var modifiedEntries = ChangeTracker.Entries()
        //            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);


        //        using var transaction = Database.BeginTransaction();

        //        foreach (var entry in modifiedEntries)
        //        {
        //            if (entry.Entity is IdentityUser<string>)
        //            {
        //                if (entry.State == EntityState.Added)
        //                {
        //                    entity.CreatedByUserId = currentUserId;
        //                    entity.CreatedDate = DateTime.UtcNow;
        //                }
        //            }
        //        }



        //        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);


        //        transaction.Commit();

        //        return result;
        //    }

        //    public override int SaveChanges()
        //    {



        //        return base.SaveChanges();
        //    }
    }
}