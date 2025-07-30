using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Config
{
    public class ExibitionConfig : IEntityTypeConfiguration<Exibition>
    {
        public void Configure(EntityTypeBuilder<Exibition> builder)
        {
            builder
            .OwnsOne(x => x.ExibitionType);
        }
    }
}
