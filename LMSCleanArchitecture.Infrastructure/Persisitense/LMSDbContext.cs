using LMSCleanArchitecture.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMSCleanArchitecture.Infrastructure.Persisitense
{
    public class LMSDbContext : IdentityDbContext<IdentityUser>
    {
        public LMSDbContext(DbContextOptions<LMSDbContext> options) : base(options) { }
        public DbSet<Courses> Courses => Set<Courses>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<StudentCourse> StudentCourse => Set<StudentCourse>();
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

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}


