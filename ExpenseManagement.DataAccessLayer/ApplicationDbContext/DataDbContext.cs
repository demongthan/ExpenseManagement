using ExpenseManagement.DataAccessLayer.ApplicationDbContext.Configurations;
using ExpenseManagement.DataAccessLayer.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ExpenseManagement.DataAccessLayer.ApplicationDbContext.Configuration;

namespace ExpenseManagement.DataAccessLayer.ApplicationDbContext
{
    public class DataDbContext : IdentityDbContext
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string text = File.ReadAllText(@"..\ExpenseManagement.DataAccessLayer\ApplicationDbContext\ConnectionString.json");
            var connectionString = JsonSerializer.Deserialize<ConnectionString>(text);

            optionsBuilder.UseSqlServer(connectionString.DefaultConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriesItemConfiguration());
            modelBuilder.ApplyConfiguration(new DailySpendingConfiguration());
            modelBuilder.ApplyConfiguration(new DebtDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new ItemDailySpendingConfiguration());
            modelBuilder.ApplyConfiguration(new LoginModelConfiguration());
            modelBuilder.ApplyConfiguration(new MonthlySpendingConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new RevenueDetailConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new SavingConfiguration());
            modelBuilder.ApplyConfiguration(new SystemParameterConfiguration());

            modelBuilder.Entity<ItemDailySpending>().ToTable(tb => tb.HasTrigger("ExpenseManagement_ProcessAfterInsertItemDailySpendings"));
            modelBuilder.Entity<ItemDailySpending>().ToTable(tb => tb.HasTrigger("ExpenseManagement_ProcessAfterUpdateItemDailySpendings"));

            modelBuilder.Entity<DebtDetails>().ToTable(tb => tb.HasTrigger("ExpenseManagement_ProcessAfterUpdateDebtDetails"));
            modelBuilder.Entity<DebtDetails>().ToTable(tb => tb.HasTrigger("ExpenseManagement_ProcessAfterInsertDebtDetails"));

            modelBuilder.Entity<RevenueDetail>().ToTable(tb => tb.HasTrigger("ExpenseManagement_ProcessAfterUpdateRevenueDetail"));
            modelBuilder.Entity<RevenueDetail>().ToTable(tb => tb.HasTrigger("ExpenseManagement_ProcessAfterInsertRevenueDetail"));
        }

        public DbSet<CategoriesItem> CategoriesItems { get; set; }
        public DbSet<DailySpending> DailySpendings { get; set; }
        public DbSet<DebtDetails> DebtDetails { get; set; }
        public DbSet<ItemDailySpending> ItemDailySpendings { get; set; }
        public DbSet<LoginModel> LoginModels { get; set; }
        public DbSet<MonthlySpending> MonthlySpendings { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RevenueDetail> RevenueDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Saving> Savings { get; set; }
        public DbSet<SystemParameter> SystemParameters { get; set; }
    }
}
