using ExpenseManagement.DataAccessLayer.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.DataAccessLayer.ApplicationDbContext.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasOne<Saving>(a => a.Saving)
                .WithOne(s => s.User)
                .HasForeignKey<Saving>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<LoginModel>(a => a.LoginModel)
                .WithOne(s => s.User)
                .HasForeignKey<LoginModel>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
