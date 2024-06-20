using ExpenseManagement.DataAccessLayer.DataModels;

namespace ExpenseManagement.DataAccessLayer.Repository.AstractClass
{
    public interface ILoginModelRepository
    {
        void UpdateLoginModelAsyn(LoginModel loginModel);
        void CreateLoginModelAsyn(LoginModel loginModel);
        Task<LoginModel> GetLoginModelAsyn(string id, bool trackChanges);
    }
}
