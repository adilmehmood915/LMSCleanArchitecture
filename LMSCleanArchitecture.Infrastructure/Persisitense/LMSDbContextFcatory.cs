using LMSCleanArchitecture.Infrastructure.Persisitense;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace LMSCleanArchitecture.Infrastructure.Persistence
{
    public class LMSDbContextFactory : IDesignTimeDbContextFactory<LMSDbContext>
    {
        public LMSDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LMSDbContext>();

            // 🔐 Replace this with your actual connection string
            var connectionString = "Server=localhost\\SQLEXPRESS;Database=LMSDbContext;Trusted_Connection=True;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);

            return new LMSDbContext(optionsBuilder.Options);
        }
    }
}
