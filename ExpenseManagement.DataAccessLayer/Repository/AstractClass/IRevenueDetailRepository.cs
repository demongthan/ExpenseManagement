using ExpenseManagement.DataAccessLayer.DataModels;

namespace ExpenseManagement.DataAccessLayer.Repository.AstractClass
{
    public interface IRevenueDetailRepository
    {
        Task<RevenueDetail> GetRevenueAsync(Guid id, bool trackChanges);
        void UpdateRevenue(RevenueDetail revenue);
        void CreateRevenue(RevenueDetail revenue);
    }
}
