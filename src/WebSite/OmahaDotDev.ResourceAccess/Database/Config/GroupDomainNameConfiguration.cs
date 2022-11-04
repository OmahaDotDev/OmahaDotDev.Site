using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmahaDotDev.ResourceAccess.Database.Model;

namespace OmahaDotDev.ResourceAccess.Database.Config
{
    internal class GroupDomainNameConfiguration : IEntityTypeConfiguration<GroupDomainNameRecord>
    {
        public void Configure(EntityTypeBuilder<GroupDomainNameRecord> builder)
        {
            builder
                .HasOne(g => g.CreatedByUser).WithMany(m => m.CreatedGroupDomainNames).OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(g => g.UpdatedByUser).WithMany(m => m.UpdatedGroupDomainNames).OnDelete(DeleteBehavior.NoAction);


        }
    }
}
