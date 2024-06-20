using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.DataAccessLayer.DataModels;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services.AstractClass
{
    public interface ISystemParameterService
    {
        Task<ApiReponse<ExpandoObject>> CreateSystemParameter(SystemParameter systemParameterEntity, string fileds);
    }
}
