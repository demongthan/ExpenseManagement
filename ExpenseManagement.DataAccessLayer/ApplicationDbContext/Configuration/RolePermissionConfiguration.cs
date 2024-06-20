using ExpenseManagement.DataAccessLayer.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManagement.DataAccessLayer.ApplicationDbContext.Configuration
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(_ => new { _.RoleId, _.PermissionId });

            builder.HasOne<Role>(rp=>rp.Role)
                .WithMany(r=>r.RolePermissions)
                .HasForeignKey(r=>r.RoleId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne<Permission>(rp => rp.Permission)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(p=>p.PermissionId)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
