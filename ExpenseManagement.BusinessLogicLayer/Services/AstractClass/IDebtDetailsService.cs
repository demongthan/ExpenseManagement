using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.DataAccessLayer.DataModels;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services.AstractClass
{
    public interface IDebtDetailsService
    {
        Task<ApiReponse<ExpandoObject>> CreateDebtDetails(DebtDetails debtDetailsEntity, string fileds);
        Task<ApiReponse<ExpandoObject>> UpdateDebtDetails(DebtDetails debtDetailsEntity, string fileds);
    }
}
