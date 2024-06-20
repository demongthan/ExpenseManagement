using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.DataAccessLayer.DataModels;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services.AstractClass
{
    public interface ISavingService
    {
        Task<ApiReponse<ExpandoObject>> UpdateSaving(Saving saving, string fileds);
    }
}
