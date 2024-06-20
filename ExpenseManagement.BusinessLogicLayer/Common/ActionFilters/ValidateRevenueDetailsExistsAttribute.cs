using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.DataAccessLayer.Repository;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseManagement.BusinessLogicLayer.Common.ActionFilters
{
    public class ValidateRevenueDetailsExistsAttribute : IAsyncActionFilter
    {
        private readonly IRevenueDetailRepository _revenueDetailRepository;
        private readonly ILoggerManager _loggerManager;

        public ValidateRevenueDetailsExistsAttribute(IRevenueDetailRepository revenueDetailRepository, ILoggerManager loggerManager)
        {
            _revenueDetailRepository = revenueDetailRepository;
            _loggerManager = loggerManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var id = (Guid)context.ActionArguments["id"];
            var revenueDetail = await _revenueDetailRepository.GetRevenueAsync(id, trackChanges);

            if (revenueDetail == null)
            {
                _loggerManager.LogInfo($"Revenue Details with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("revenueDetail", revenueDetail);
                await next();
            }
        }
    }
}
