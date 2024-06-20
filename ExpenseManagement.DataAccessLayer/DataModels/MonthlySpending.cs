using ExpenseManagement.DataAccessLayer.DataModels.Common;

namespace ExpenseManagement.DataAccessLayer.DataModels
{
    public class MonthlySpending : DateTimeCommon
    {
        public Guid Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int BudgetSpent { get; set; }
        public Guid IdSaving { get; set; }
        public Saving Saving { get; set; }
        public virtual IEnumerable<DailySpending> DailySpendings { get; set; }
    }
}
