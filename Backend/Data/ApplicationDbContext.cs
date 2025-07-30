using Backend.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<FemaleEmployee> FemaleEmployees { get; set; }
        public DbSet<MaleEmployee> MaleEmployees { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Pregnancy> Pregnancies { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<NotificationRecipient> NotificationRecipients { get; set; }
        public DbSet<Manifestation> Manifestations { get; set; }
        public DbSet<Exibition> Exibitions { get; set; }
        public DbSet<ManifestationRegistration> ManifestationRegistrations { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
    }
}
