using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.DataAccessLayer.DataModels;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services.AstractClass
{
    public interface IRevenueDetailsService
    {
        Task<ApiReponse<ExpandoObject>> CreateRevenue(RevenueDetail revenueDetail, string? fileds);
        Task<ApiReponse<ExpandoObject>> UpdateRevenue(RevenueDetail revenueDetail, string? fileds);
    }
}
