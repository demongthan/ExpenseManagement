namespace ExpenseManagement.DataAccessLayer.Repository.RepositoryParameters
{
    public class SystemParameterRP : BaseRP
    {
        public SystemParameterRP(int pageNumber, int pageSize, string orderBy, string searchTerm) : base(pageNumber, pageSize, orderBy, searchTerm)
        {

        }
    }
}
