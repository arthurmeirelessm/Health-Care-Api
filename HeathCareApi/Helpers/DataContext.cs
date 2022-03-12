using HealthCareApi.Entities;
using HealthCareApi.Enuns;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApi.Helpers
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
           

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .Property(e => e.TypeUser)
                .HasConversion(
                v => v.ToString(),
                v => (TypeUser)Enum.Parse(typeof(TypeUser), v));
        }
        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                DateTime dateTime = DateTime.Now;
                ((BaseEntity)entityEntry.Entity).UpdateAt = dateTime;
                if (entityEntry.State == EntityState.Added)
                    ((BaseEntity)entityEntry.Entity).CreatedId = dateTime;
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        public DbSet<NoteForMedicalCare> NoteForMedicalCares { get; set; }
        public DbSet<Specialty> Specialtys { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
