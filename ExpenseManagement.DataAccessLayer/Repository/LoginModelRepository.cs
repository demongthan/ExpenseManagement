using ExpenseManagement.DataAccessLayer.ApplicationDbContext;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.DataAccessLayer.Repository
{
    public class LoginModelRepository : BaseRepository<LoginModel>, ILoginModelRepository
    {
        private readonly DataDbContext _dbContext;
        public LoginModelRepository(DataDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateLoginModelAsyn(LoginModel loginModel) => Create(loginModel);
        public void UpdateLoginModelAsyn(LoginModel loginModel) => Update(loginModel);
        public async Task<LoginModel> GetLoginModelAsyn(string id, bool trackChanges) => await FindByCondition(p => p.UserId.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}
