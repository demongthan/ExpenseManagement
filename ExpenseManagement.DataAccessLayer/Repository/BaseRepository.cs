using ExpenseManagement.DataAccessLayer.ApplicationDbContext;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.DataAccessLayer.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DataDbContext _dbContext;

        public BaseRepository(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(T entity) => _dbContext.Set<T>().Add(entity);

        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();

        public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool trackChanges)
             => !trackChanges ? _dbContext.Set<T>().Where(expression).AsNoTracking() : _dbContext.Set<T>().Where(expression);

        public void Update(T entity) => _dbContext.Set<T>().Update(entity);
    }
}
