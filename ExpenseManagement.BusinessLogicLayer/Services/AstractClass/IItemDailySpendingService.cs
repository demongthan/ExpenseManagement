using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.DataAccessLayer.DataModels;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services.AstractClass
{
    public interface IItemDailySpendingService
    {
        Task<ApiReponse<ExpandoObject>> CreateItemDailySpending(ItemDailySpending itemDailySpending, string fileds);
        Task<ApiReponse<ExpandoObject>> UpdateItemDailySpending(ItemDailySpending itemDailySpending, string fileds);
    }
}
