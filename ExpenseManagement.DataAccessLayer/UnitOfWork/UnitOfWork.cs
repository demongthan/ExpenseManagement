using ExpenseManagement.DataAccessLayer.ApplicationDbContext;
using ExpenseManagement.DataAccessLayer.UnitOfWork.AstractClass;

namespace ExpenseManagement.DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataDbContext _dbContext;

        public UnitOfWork(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<bool> SaveChangesAsync()
        {
            bool returnValue = true;
            //This CreateTransaction() method will create a database Trnasaction so that we can do database operations by
            //applying do evrything and do nothing principle
            using (var dbContextTransaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await _dbContext.SaveChangesAsync();

                    //If all the Transactions are completed successfuly then we need to call this Commit() 
                    //method to Save the changes permanently in the database
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    returnValue = false;
                    //If atleast one of the Transaction is Failed then we need to call this Rollback() 
                    //method to Rollback the database changes to its previous state
                    dbContextTransaction.Rollback();
                    dbContextTransaction.Dispose();
                }
            }
            return returnValue;
        }

        public async Task DisposeAsync() => await _dbContext.DisposeAsync();
    }
}
