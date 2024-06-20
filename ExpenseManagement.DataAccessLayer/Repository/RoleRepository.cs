using ExpenseManagement.DataAccessLayer.ApplicationDbContext;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.DataAccessLayer.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly DataDbContext _dbContext;

        public RoleRepository(DataDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateRoleAsyn(Role role) => Create(role);

        public async Task<Role> GetRoleAsynByCode(string code, bool trackChanges) => await FindByCondition(p => p.Code.Equals(code), trackChanges).SingleOrDefaultAsync();
    }
}
