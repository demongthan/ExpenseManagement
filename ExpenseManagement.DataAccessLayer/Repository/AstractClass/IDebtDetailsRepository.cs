using ExpenseManagement.DataAccessLayer.DataModels;

namespace ExpenseManagement.DataAccessLayer.Repository.AstractClass
{
    public interface IDebtDetailsRepository
    {
        Task<DebtDetails> GetDebtAsync(Guid id, bool trackChanges);
        void UpdateDebt(DebtDetails debtDetails);
        void CreateDebt(DebtDetails debtDetails);
    }
}
