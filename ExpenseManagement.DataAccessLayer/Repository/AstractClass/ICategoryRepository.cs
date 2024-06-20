using ExpenseManagement.DataAccessLayer.DataModels;

namespace ExpenseManagement.DataAccessLayer.Repository.AstractClass
{
    public interface ICategoryRepository
    {
        Task<CategoriesItem> GetCategoriesAsync(Guid id, bool trackChanges);
        void UpdateCategory(CategoriesItem category);
        void CreateCategory(CategoriesItem category);
    }
}
