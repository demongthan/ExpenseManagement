using ExpenseManagement.DataAccessLayer.ApplicationDbContext;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.DataAccessLayer.Repository
{
    public class RevenueDetailRepository : BaseRepository<RevenueDetail>, IRevenueDetailRepository
    {
        private readonly DataDbContext _dbContext;
        public RevenueDetailRepository(DataDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateRevenue(RevenueDetail revenue) => Create(revenue);

        public void UpdateRevenue(RevenueDetail revenue) => Update(revenue);

        public async Task<RevenueDetail> GetRevenueAsync(Guid id, bool trackChanges) => await FindByCondition(p => p.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}
