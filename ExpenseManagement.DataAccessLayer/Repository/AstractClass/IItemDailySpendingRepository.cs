using ExpenseManagement.DataAccessLayer.DataModels;

namespace ExpenseManagement.DataAccessLayer.Repository.AstractClass
{
    public interface IItemDailySpendingRepository
    {
        public void CreateItemDailySpending(ItemDailySpending itemDailySpending);
        Task<ItemDailySpending> GetItemDailySpendingAsync(Guid id, bool trackChanges);
        void UpdateItemDailySpending(ItemDailySpending itemDailySpending);
    }
}
