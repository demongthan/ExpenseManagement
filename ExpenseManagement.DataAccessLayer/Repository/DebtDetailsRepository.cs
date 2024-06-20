using ExpenseManagement.DataAccessLayer.ApplicationDbContext;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.DataAccessLayer.Repository
{
    public class DebtDetailsRepository : BaseRepository<DebtDetails>, IDebtDetailsRepository
    {
        private readonly DataDbContext _dbContext;
        public DebtDetailsRepository(DataDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateDebt(DebtDetails debtDetails) => Create(debtDetails);

        public void UpdateDebt(DebtDetails debtDetails) => Update(debtDetails);

        public async Task<DebtDetails> GetDebtAsync(Guid id, bool trackChanges) => await FindByCondition(p => p.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}
