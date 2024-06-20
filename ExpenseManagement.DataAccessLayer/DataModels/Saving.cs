using ExpenseManagement.DataAccessLayer.DataModels.Common;

namespace ExpenseManagement.DataAccessLayer.DataModels
{
    public class Saving : DateTimeCommon
    {
        public Guid Id { get; set; }
        public int SavingsAvailable { get; set; }
        public int UltimateSavings { get; set; }
        public int Debts { get; set; }
        public int Revenues { get; set; }
        public int Expenses { get; set; }
        public int CashSaving { get; set; }
        public int CreditSaving { get; set; }
        public string UserId { get; set; }
        public Account User { get; set; }
        public virtual IEnumerable<DebtDetails> DebtDetails { get; set; }
        public virtual IEnumerable<RevenueDetail> RevenueDetails { get; set; }
        public virtual IEnumerable<MonthlySpending> MonthlySpendings { get; set; }
        public virtual IEnumerable<>
    }
}