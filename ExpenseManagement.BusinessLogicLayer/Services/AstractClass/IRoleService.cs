using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.DataAccessLayer.DataModels;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services.AstractClass
{
    public interface IRoleService
    {
        Task<ApiReponse<ExpandoObject>> CreateRole(Role roleEntity, string fileds);
    }
}
