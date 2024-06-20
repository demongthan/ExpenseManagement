using ExpenseManagement.DataAccessLayer.ApplicationDbContext;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.DataAccessLayer.Repository
{
    public class SavingRepository : BaseRepository<Saving>, ISavingRepository
    {
        private readonly DataDbContext _dbContext;
        public SavingRepository(DataDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void UpdateSaving(Saving entity) => Update(entity);

        public async Task<Saving> GetSavingAsyn(Guid id, bool trackChanges) => await FindByCondition(p => p.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}
