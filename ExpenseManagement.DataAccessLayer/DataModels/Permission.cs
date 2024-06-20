using ExpenseManagement.DataAccessLayer.DataModels.Common;

namespace ExpenseManagement.DataAccessLayer.DataModels
{
    public class Permission : DateTimeCommon
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public virtual IEnumerable<RolePermission> RolePermissions { get; set; }
    }
}
