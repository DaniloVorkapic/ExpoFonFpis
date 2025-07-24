using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Config
{
    public class EmployeeDbConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasDiscriminator<string>("EmployeeType")
                .HasValue<MaleEmployee>("Male")
                .HasValue<FemaleEmployee>("Female");

            builder
                .HasMany(e => e.Children)
                .WithOne()
                .HasForeignKey("EmployeeId");

        }
    }
}
