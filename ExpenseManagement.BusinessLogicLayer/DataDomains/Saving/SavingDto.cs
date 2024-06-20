using ExpenseManagement.DataAccessLayer.DataModels;

namespace ExpenseManagement.BusinessLogicLayer.DataDomains.Saving
{
    public class SavingDto
    {
        public Guid Id { get; set; }
        public int SavingsAvailable { get; set; }
        public int UltimateSavings { get; set; }
        public int Debts { get; set; }
        public int Revenues { get; set; }
        public int Expenses { get; set; }
        public int CashSaving { get; set; }
        public int CreditSaving { get; set; }
    }
}
