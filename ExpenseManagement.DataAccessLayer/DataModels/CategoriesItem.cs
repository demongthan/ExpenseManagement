using ExpenseManagement.DataAccessLayer.DataModels.Common;

namespace ExpenseManagement.DataAccessLayer.DataModels
{
    public class CategoriesItem : DateTimeCommon
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual IEnumerable<ItemDailySpending> ItemDailySpendings { get; set; }
    }
}
