using ExpenseManagement.DataAccessLayer.DataModels.Common;

namespace ExpenseManagement.DataAccessLayer.DataModels
{
    public class SystemParameter : DateTimeCommon
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Content { get; set; }
        public string? Description { get; set; }
    }
}
