using ExpenseManagement.DataAccessLayer.ApplicationDbContext;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.DataAccessLayer.Repository
{
    public class CategoryRepository : BaseRepository<CategoriesItem>, ICategoryRepository
    {
        private readonly DataDbContext _dbContext;
        public CategoryRepository(DataDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateCategory(CategoriesItem category) => Create(category);

        public void UpdateCategory(CategoriesItem category) => Update(category);

        public async Task<CategoriesItem> GetCategoriesAsync(Guid id, bool trackChanges) => await FindByCondition(p => p.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}
