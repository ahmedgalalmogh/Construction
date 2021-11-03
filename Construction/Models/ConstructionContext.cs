using Microsoft.EntityFrameworkCore;



namespace Construction.Models
{
    public class ConstructionContext :DbContext
    {
        public ConstructionContext(DbContextOptions<ConstructionContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            // configures one-to-many relationship
            modelBuilder.Entity<Building>()
              .HasOne(p => p.project)
              .WithMany(b => b.Buildings)
              .HasForeignKey(p => p.projectId);
            modelBuilder.Entity<Unit>()
             .HasOne(p => p.building)
             .WithMany(b => b.units)
             .HasForeignKey(p => p.BuildingId);

        }
    public DbSet<PoneNumber> projects { get; set; }
        public DbSet<Building> buildings { get; set; }
        public DbSet<Unit> units { get; set; }
        public DbSet<PhoneNumber> phoneNumbers { get; set; }

    }
}
