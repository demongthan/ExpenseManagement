using ExpenseManagement.DataAccessLayer.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.DataAccessLayer.ApplicationDbContext.Configurations
{
    public class MonthlySpendingConfiguration : IEntityTypeConfiguration<MonthlySpending>
    {
        public void Configure(EntityTypeBuilder<MonthlySpending> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.Month)
                .HasColumnType("int")
                .HasDefaultValueSql("Month(GETUTCDATE())");

            builder.Property(_ => _.Year)
                .HasColumnType("int")
                .HasDefaultValueSql("Year(GETUTCDATE())");

            builder.Property(_ => _.BudgetSpent)
                .HasColumnType("int")
                .HasDefaultValue(0);

            builder.HasMany(m => m.DailySpendings)
                .WithOne(d => d.MonthlySpending)
                .HasForeignKey(d => d.MonthlySpendingId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Property(_ => _.UserCreate)
                .HasColumnType("nvarchar(50)");

            builder.Property(_ => _.UserUpdate)
                .HasColumnType("nvarchar(50)");

            builder.Property(_ => _.CreatedDateUtc)
                .HasColumnType("datetime2(7)")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(_ => _.UpdatedDateUtc)
                .HasColumnType("datetime2(7)");

        }
    }
}
