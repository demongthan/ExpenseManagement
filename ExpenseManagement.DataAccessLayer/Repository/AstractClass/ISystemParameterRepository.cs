using ExpenseManagement.DataAccessLayer.Common;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.RepositoryParameters;

namespace ExpenseManagement.DataAccessLayer.Repository.AstractClass
{
    public interface ISystemParameterRepository
    {
        Task<PagedList<SystemParameter>> GetAllSystemParameterAsyn(SystemParameterRP systemParameterRP, bool trackChanges);

        void CreateSystemParameterAsyn(SystemParameter systemParameter);

        Task<SystemParameter> GetSystemParameterAsyn(Guid id, bool trackChanges);

        Task<SystemParameter> GetSystemParameterAsynByCode(string code, bool trackChanges);
    }
}
