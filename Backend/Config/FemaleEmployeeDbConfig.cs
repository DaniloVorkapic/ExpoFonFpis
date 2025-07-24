using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Config
{
    public class FemaleEmployeeDbConfig : IEntityTypeConfiguration<FemaleEmployee>
    {
        public void Configure(EntityTypeBuilder<FemaleEmployee> builder)
        {
            builder
                .HasMany(f => f.Pregnancies)
                .WithOne()
                .HasForeignKey("FemaleEmployeeId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(f => f.ReturnDate, rd =>
            {
                rd.Property(r => r.Date)
                    .HasColumnName("ReturnDate");
            });
        }
    }
}
