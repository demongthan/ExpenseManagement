namespace ExpenseManagement.DataAccessLayer.Repository.RepositoryParameters
{
    public class BaseRP
    {
        private int pageNumber;
        private int pageSize;
        private string orderBy;
        private string searchTerm;

        public BaseRP(int pageNumber, int pageSize, string orderBy, string searchTerm)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            this.orderBy = orderBy;
            this.searchTerm = searchTerm;
        }

        public int PageNumber { get => pageNumber; set => pageNumber = value; }
        public int PageSize { get => pageSize; set => pageSize = value; }
        public string OrderBy { get => orderBy; set => orderBy = value; }
        public string SearchTerm { get => searchTerm; set => searchTerm = value; }
    }
}
