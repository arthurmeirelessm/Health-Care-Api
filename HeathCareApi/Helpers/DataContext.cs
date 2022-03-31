using HealthCareApi.Entities;
using HealthCareApi.Enuns;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApi.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            if (!options.IsConfigured)

            {

                options.UseSqlServer("A FALLBACK CONNECTION STRING");

            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .Property(e => e.TypeUser)
                .HasConversion(
                v => v.ToString(),
                v => (TypeUser)Enum.Parse(typeof(TypeUser), v));

            builder.Entity<Specialty>()
                .HasOne(e => e.Doctor)
                .WithMany(c => c.SpecialtiesDoctorChiefing)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Specialty>()
               .HasMany(p => p.Patients)
               .WithMany(t => t.SpecialtiesActived)
               .UsingEntity<PatientSpecialty>(
                j => j
                     .HasOne(pt => pt.Patient)
                     .WithMany(t => t.PatientSpecialties)
                     .HasForeignKey(pt => pt.PatientId),
                j => j
                     .HasOne(pt => pt.Specialty)
                     .WithMany(p => p.PatientSpecialties)
                     .HasForeignKey(pt => pt.SpecialtyId),
                j =>
                {
                    j.HasKey(t => new { t.SpecialtyId, t.PatientId });
                });
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
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
