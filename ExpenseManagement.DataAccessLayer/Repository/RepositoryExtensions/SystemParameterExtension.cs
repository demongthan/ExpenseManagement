using ExpenseManagement.DataAccessLayer.Common.Utility;
using ExpenseManagement.DataAccessLayer.DataModels;
using System.Linq.Dynamic.Core;

namespace ExpenseManagement.DataAccessLayer.Repository.RepositoryExtensions
{
    public static class SystemParameterExtension
    {
        public static IQueryable<SystemParameter> PagedSystemParameter(this IQueryable<SystemParameter> systemParameters, int pageNumber, int pageSize) => systemParameters.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        public static IQueryable<SystemParameter> Search(this IQueryable<SystemParameter> systemParameters, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return systemParameters;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return systemParameters.Where(e => e.Code.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<SystemParameter> Sort(this IQueryable<SystemParameter> systemParameters, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return systemParameters.OrderBy(e => e.Code);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<SystemParameter>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return systemParameters.OrderBy(e => e.Code);
            return systemParameters.OrderBy(orderQuery);
        }
    }
}
