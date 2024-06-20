using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseManagement.BusinessLogicLayer.Common.ActionFilters
{
    public class ValidateSavingExistsAttribute : IAsyncActionFilter
    {
        private readonly ISavingRepository _savingRepository;
        private readonly ILoggerManager _loggerManager;

        public ValidateSavingExistsAttribute(ISavingRepository savingRepository, ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
            _savingRepository = savingRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var id = (Guid)context.ActionArguments["id"];
            var saving = await _savingRepository.GetSavingAsyn(id, trackChanges);

            if (saving == null)
            {
                _loggerManager.LogInfo($"Saving with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("saving", saving);
                await next();
            }
        }
    }
}
