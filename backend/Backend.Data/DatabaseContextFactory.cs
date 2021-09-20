using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Backend.Data
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            //optionsBuilder.UseSqlServer("ConnectionString");
            optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=TestDB;User Id=SA;Password=StrongPassword123");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}