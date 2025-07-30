using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Config
{
    public class ManifestationDbConfig : IEntityTypeConfiguration<Manifestation>
    {
        public void Configure(EntityTypeBuilder<Manifestation> builder)
        {
            builder
                .HasMany(m => m.ManifestationRegistrations)
                .WithOne(x => x.Manifestation)
                .HasForeignKey(x => x.ManifestationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                 .HasMany(m => m.Exibitions)
                .WithOne(x => x.Manifestation)
                .HasForeignKey(x => x.ManifestationId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
