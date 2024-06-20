using ExpenseManagement.DataAccessLayer.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.DataAccessLayer.ApplicationDbContext.Configurations
{
    public class SavingConfiguration : IEntityTypeConfiguration<Saving>
    {
        public void Configure(EntityTypeBuilder<Saving> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.Debts)
                .HasColumnType("int")
                .HasDefaultValue(0);

            builder.Property(_ => _.SavingsAvailable)
                .HasColumnType("int")
                .HasDefaultValue(0);

            builder.Property(_ => _.UltimateSavings)
                .HasColumnType("int")
                .HasDefaultValue(0);

            builder.Property(_ => _.Debts)
                .HasColumnType("int")
                .HasDefaultValue(0);

            builder.Property(_ => _.Revenues)
                .HasColumnType("int")
                .HasDefaultValue(0);

            builder.Property(_ => _.CashSaving)
               .HasColumnType("int")
               .HasDefaultValue(0);

            builder.Property(_ => _.CreditSaving)
               .HasColumnType("int")
               .HasDefaultValue(0);

            builder.Property(_ => _.Expenses)
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

            builder.HasMany(_ => _.DebtDetails)
                .WithOne(d => d.Saving)
               .HasForeignKey(d => d.IdSaving)
               .OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasMany(_ => _.RevenueDetails)
                .WithOne(r => r.Saving)
               .HasForeignKey(r => r.IdSaving)
               .OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasMany(_ => _.MonthlySpendings)
                .WithOne(m => m.Saving)
               .HasForeignKey(m => m.IdSaving)
               .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
