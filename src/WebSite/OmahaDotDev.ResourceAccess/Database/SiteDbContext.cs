using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OmahaDotDev.Model.Common;
using OmahaDotDev.ResourceAccess.Database.Entities;

namespace OmahaDotDev.ResourceAccess.Database
{
    internal class SiteDbContext : DbContext
    {
        private readonly AmbientContext _ambientContext;
        //private readonly ITimeUtility _timeUtility;
        public SiteDbContext(DbContextOptions<SiteDbContext> options, AmbientContext ambientContext/*, ITimeUtility timeUtility*/)
            : base(options)
        {
            _ambientContext = ambientContext;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("groups");
        }

        public DbSet<GroupRecord> Groups { get; set; }
        public DbSet<MemberRecord> Members { get; set; }
        public DbSet<GroupDomainName> GroupDomainNames { get; set; }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {

            if (!_ambientContext.IsLoggedIn || _ambientContext.UserId.IsNullOrEmpty())
            {
                throw new InvalidOperationException("Unable to save auditable entity unless a user is logged in");
            }

            string currentUserId = _ambientContext.UserId!;

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is AuditableEntity entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedByUserId = currentUserId;
                        entity.CreatedDate = DateTime.UtcNow;
                    }

                    entity.UpdatedByUserId = currentUserId;
                    entity.UpdatedDate = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            if (!_ambientContext.IsLoggedIn || _ambientContext.UserId.IsNullOrEmpty())
            {
                throw new InvalidOperationException("Unable to save auditable entity unless a user is logged in");
            }

            string currentUserId = _ambientContext.UserId!;

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is AuditableEntity entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedByUserId = currentUserId;
                        entity.CreatedDate = DateTime.UtcNow;
                    }

                    entity.UpdatedByUserId = currentUserId;
                    entity.UpdatedDate = DateTime.UtcNow;
                }
            }


            return base.SaveChanges();
        }
    }
}
