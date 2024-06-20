using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.DataAccessLayer.DataModels;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services.AstractClass
{
    public interface ICategoryService
    {
        Task<ApiReponse<ExpandoObject>> UpdateCategory(CategoriesItem categoriesItem, string? fileds);
        Task<ApiReponse<ExpandoObject>> CreateCategory(CategoriesItem categoriesItemEntity, string? fileds);
    }
}
