using Backend.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
        }
    }
}
