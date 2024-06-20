using ExpenseManagement.DataAccessLayer.DataModels.Common;

namespace ExpenseManagement.DataAccessLayer.DataModels
{
    public class DailySpending : DateTimeCommon
    {
        public Guid Id { get; set; }
        public int Day { get; set; }
        public Guid MonthlySpendingId { get; set; }
        public MonthlySpending MonthlySpending { get; set; }
        public int Mining { get; set; }
        public virtual IEnumerable<ItemDailySpending> ItemDailySpendings { get; set; }
    }
}
