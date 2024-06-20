using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseManagement.BusinessLogicLayer.Common.ActionFilters
{

    public class ValidateCategoryExistsAttribute : IAsyncActionFilter
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILoggerManager _loggerManager;

        public ValidateCategoryExistsAttribute(ICategoryRepository categoryRepository, ILoggerManager loggerManager)
        {
            _categoryRepository = categoryRepository;
            _loggerManager = loggerManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var id = (Guid)context.ActionArguments["id"];
            var category = await _categoryRepository.GetCategoriesAsync(id, trackChanges);

            if (category == null)
            {
                _loggerManager.LogInfo($"Category with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("category", category);
                await next();
            }
        }
    }
}
