using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseManagement.BusinessLogicLayer.Common.ActionFilters
{
    public class ValidateDebtDetailsExistsAttribute : IAsyncActionFilter
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IDebtDetailsRepository _debtDetailsRepository;

        public ValidateDebtDetailsExistsAttribute(ILoggerManager loggerManager, IDebtDetailsRepository debtDetailsRepository)
        {
            _loggerManager = loggerManager;
            _debtDetailsRepository = debtDetailsRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var id = (Guid)context.ActionArguments["id"];
            var debtDetails = await _debtDetailsRepository.GetDebtAsync(id, trackChanges);

            if (debtDetails == null)
            {
                _loggerManager.LogInfo($"Debt Details with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("debtDetails", debtDetails);
                await next();
            }
        }
    }
}
