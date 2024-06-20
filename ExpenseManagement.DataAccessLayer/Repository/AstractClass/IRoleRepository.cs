using ExpenseManagement.DataAccessLayer.DataModels;

namespace ExpenseManagement.DataAccessLayer.Repository.AstractClass
{
    public interface IRoleRepository
    {
        void CreateRoleAsyn(Role role);
        Task<Role> GetRoleAsynByCode(string code, bool trackChanges);
    }
}
