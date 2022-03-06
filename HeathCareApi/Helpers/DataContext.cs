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


        public DbSet<NoteForMedicalCare> NoteForMedicalCares { get; set; }
        public DbSet<Specialty> Specialtys { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
