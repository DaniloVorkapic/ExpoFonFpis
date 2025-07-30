using Backend.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using MassTransit.Configuration;
using Microsoft.Data.SqlClient;

namespace Backend.Config
{
    public class ManifestationRegistrationDbConfig : IEntityTypeConfiguration<ManifestationRegistration>
    {
        public void Configure(EntityTypeBuilder<ManifestationRegistration> builder)
        {
            builder
            .OwnsOne(m => m.Address, address =>
            {
                address.WithOwner();
                address.Property(a => a.StreetOne).HasColumnName("StreetOne");
                address.Property(a => a.StreetTwo).HasColumnName("StreetTwo");
                address.Property(a => a.PostCode).HasColumnName("PostCode");
                address.Property(a => a.CityName).HasColumnName("CityName");
                address.Property(a => a.Country).HasColumnName("Country");
            });

            builder
            .HasOne(r => r.PromoCodeGenerated)
            .WithOne(pc => pc.ManifestationRegistration)
            .HasForeignKey<PromoCode>(pc => pc.ManifestationRegistrationId);

            builder
              .OwnsOne(x => x.LifecycleStatus);
        }
    }
}
