using ExpenseManagement.DataAccessLayer.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.DataAccessLayer.ApplicationDbContext.Configurations
{
    public class SystemParameterConfiguration : IEntityTypeConfiguration<SystemParameter>
    {
        public void Configure(EntityTypeBuilder<SystemParameter> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            builder.Property(_ => _.Code)
                .HasColumnType("nvarchar(50)");

            builder.Property(_=>_.Content)
                 .HasColumnType("nvarchar(500)");

            builder.Property(_=>_.Description)
                .HasColumnType("nvarchar(max)");

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
