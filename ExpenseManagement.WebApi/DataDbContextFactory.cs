using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using ExpenseManagement.DataAccessLayer.ApplicationDbContext;

namespace WebSales_BackEnd.WebAPI
{
    public class DataDbContextFactory : IDesignTimeDbContextFactory<DataDbContext>
    {
        private readonly IConfiguration _configuration;

        public DataDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

            return new DataDbContext(optionsBuilder.Options);
        }
    }
}

