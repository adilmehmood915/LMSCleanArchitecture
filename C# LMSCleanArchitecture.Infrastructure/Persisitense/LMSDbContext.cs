using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using LMSCleanArchitecture.Core.Entities;

namespace LMSCleanArchitecture.Infrastructure.Persisitense
{
    public class LMSDbContext : DbContext
    {
        public LMSDbContext(DbContextOptions<LMSDbContext> options) : base(options) { }
        public DbSet<Courses> Courses => Set<Courses>();
        public DbSet<Instructor> Instructors => Set<Instructor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Persist Courses as JSON in nvarchar(max)
            var converter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => string.IsNullOrWhiteSpace(v) ? new List<string>() :
                     (JsonSerializer.Deserialize<List<string>>(v) ?? new List<string>()));

            var comparer = new ValueComparer<List<string>>(
                (l, r) => l.SequenceEqual(r),
                l => l.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                l => l.ToList());

            modelBuilder.Entity<Instructor>()
                .Property(e => e.Courses)
                .HasConversion(converter)
                .Metadata.SetValueComparer(comparer);
        }
    }
}