using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmahaDotDev.ResourceAccess.Database.Model;

namespace OmahaDotDev.ResourceAccess.Database.Config
{
    internal class MemberConfiguration : IEntityTypeConfiguration<MemberRecord>
    {
        public void Configure(EntityTypeBuilder<MemberRecord> builder)
        {
            builder
                .HasMany(m => m.CreatedGroups).WithOne(g => g.CreatedByUser);
            builder
                .HasMany(m => m.UpdatedGroups).WithOne(g => g.UpdatedByUser);
        }
    }
}
