using ExpenseManagement.DataAccessLayer.DataModels;

namespace ExpenseManagement.DataAccessLayer.Repository.AstractClass
{
    public interface ISavingRepository
    {
        void UpdateSaving(Saving entity);

        Task<Saving> GetSavingAsyn(Guid id, bool trackChanges);
    }
}
