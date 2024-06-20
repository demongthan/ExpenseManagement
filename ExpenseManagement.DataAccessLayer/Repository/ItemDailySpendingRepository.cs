using ExpenseManagement.DataAccessLayer.ApplicationDbContext;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.DataAccessLayer.Repository
{
    public class ItemDailySpendingRepository : BaseRepository<ItemDailySpending>, IItemDailySpendingRepository
    {
        private readonly DataDbContext _dbContext;

        public ItemDailySpendingRepository(DataDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateItemDailySpending(ItemDailySpending itemDailySpending) => Create(itemDailySpending);

        public void UpdateItemDailySpending(ItemDailySpending itemDailySpending) => Update(itemDailySpending);

        public async Task<ItemDailySpending> GetItemDailySpendingAsync(Guid id, bool trackChanges) => await FindByCondition(p => p.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}
