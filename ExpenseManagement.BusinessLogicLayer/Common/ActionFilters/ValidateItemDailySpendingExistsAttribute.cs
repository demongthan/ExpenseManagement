using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseManagement.BusinessLogicLayer.Common.ActionFilters
{
    public class ValidateItemDailySpendingExistsAttribute : IAsyncActionFilter
    {
        private readonly IItemDailySpendingRepository _itemDailySpendingRepository;
        private readonly ILoggerManager _loggerManager;

        public ValidateItemDailySpendingExistsAttribute(IItemDailySpendingRepository itemDailySpendingRepository, ILoggerManager loggerManager)
        {
            _itemDailySpendingRepository = itemDailySpendingRepository;
            _loggerManager = loggerManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var id = (Guid)context.ActionArguments["id"];
            var itemDailySpending = await _itemDailySpendingRepository.GetItemDailySpendingAsync(id, trackChanges);

            if (itemDailySpending == null)
            {
                _loggerManager.LogInfo($"Item Daily Spending with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("itemDailySpending", itemDailySpending);
                await next();
            }
        }
    }
}
