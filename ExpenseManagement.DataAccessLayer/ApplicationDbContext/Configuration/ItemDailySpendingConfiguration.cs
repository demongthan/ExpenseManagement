using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.DataModels.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.DataAccessLayer.ApplicationDbContext.Configuration
{
    public class ItemDailySpendingConfiguration : IEntityTypeConfiguration<ItemDailySpending>
    {
        public void Configure(EntityTypeBuilder<ItemDailySpending> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.Name)
                .HasColumnType("nvarchar(50)");

            builder.Property(_ => _.Description)
                .HasColumnType("nvarchar(max)");

            builder.Property(_ => _.Mining)
                .HasColumnType("int")
                .HasDefaultValue(0);

            builder.Property(_ => _.BeforeMining)
                .HasColumnType("int")
                .HasDefaultValue(0);

            builder.Property(_ => _.PaymentMethod)
                .HasColumnType("tinyint")
                .HasDefaultValue(PaymentMethod.Cash);

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
