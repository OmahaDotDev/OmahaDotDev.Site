using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmahaDotDev.ResourceAccess.Database.Entities;

namespace OmahaDotDev.ResourceAccess.Database.Config
{
    internal class GroupConfiguration : IEntityTypeConfiguration<GroupRecord>
    {
        public void Configure(EntityTypeBuilder<GroupRecord> builder)
        {
            throw new NotImplementedException();
        }
    }
}
