using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Config
{
    public class PregnancyDbConfig : IEntityTypeConfiguration<Pregnancy>
    {
        public void Configure(EntityTypeBuilder<Pregnancy> builder)
        {
            builder
                .HasMany(p => p.Leaves)
                .WithOne()
                .HasForeignKey("PregnancyId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
