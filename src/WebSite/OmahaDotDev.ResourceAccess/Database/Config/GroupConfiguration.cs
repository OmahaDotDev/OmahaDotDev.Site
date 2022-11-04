using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmahaDotDev.ResourceAccess.Database.Model;

namespace OmahaDotDev.ResourceAccess.Database.Config
{
    internal class GroupConfiguration : IEntityTypeConfiguration<GroupRecord>
    {
        public void Configure(EntityTypeBuilder<GroupRecord> builder)
        {
            builder
                .HasOne(g => g.CreatedByUser).WithMany(m => m.CreatedGroups).OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(g => g.UpdatedByUser).WithMany(m => m.UpdatedGroups).OnDelete(DeleteBehavior.NoAction);


        }
    }
}
