namespace ExpenseManagement.DataAccessLayer.UnitOfWork.AstractClass
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveChangesAsync();
        Task DisposeAsync();
    }
}
