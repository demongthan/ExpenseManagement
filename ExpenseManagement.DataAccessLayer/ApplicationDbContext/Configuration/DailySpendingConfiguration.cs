using ExpenseManagement.DataAccessLayer.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.DataAccessLayer.ApplicationDbContext.Configurations
{
    public class DailySpendingConfiguration : IEntityTypeConfiguration<DailySpending>
    {
        public void Configure(EntityTypeBuilder<DailySpending> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.Day)
                .HasColumnType("int")
                .HasDefaultValueSql("Day(GETUTCDATE())");

            builder.Property(_ => _.Mining)
                .HasColumnType("int")
                .HasDefaultValue(0);

            builder.Property(_ => _.UserCreate)
                .HasColumnType("nvarchar(50)");

            builder.Property(_ => _.UserUpdate)
                .HasColumnType("nvarchar(50)");

            builder.Property(_ => _.CreatedDateUtc)
                .HasColumnType("datetime2(7)")
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(_ => _.UpdatedDateUtc)
                .HasColumnType("datetime2(7)");

            builder.HasMany(_ => _.ItemDailySpendings)
                .WithOne(i => i.DailySpending)
               .HasForeignKey(i => i.IdDailySpending)
               .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
