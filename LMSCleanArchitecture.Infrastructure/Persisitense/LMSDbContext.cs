using LMSCleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMSCleanArchitecture.Infrastructure.Persisitense
{
    public class LMSDbContext : DbContext
    {
        public LMSDbContext(DbContextOptions<LMSDbContext> options) : base(options) { }

        public DbSet<Courses> Courses => Set<Courses>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Student> Students => Set<Student>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Courses>()
                .HasMany(c => c.Instructors)
                .WithMany(i => i.Courses)
                .UsingEntity(j =>
                {
                    j.ToTable("CourseInstructors"); 
                });
        }
    }
}
