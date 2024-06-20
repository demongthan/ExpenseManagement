using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseManagement.BusinessLogicLayer.Common.ActionFilters
{
    public class ValidateUserExistsAttribute : IAsyncActionFilter
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILoggerManager _loggerManager;

        public ValidateUserExistsAttribute(UserManager<IdentityUser> userManager, ILoggerManager loggerManager)
        {
            _userManager = userManager;
            _loggerManager = loggerManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            if (method.Equals("POST"))
            {
                var param = (RegisterDto)context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
                var userExists = await _userManager.FindByNameAsync(param.Username);

                if (userExists != null)
                {
                    _loggerManager.LogError($"User Name with User Name: {param.Username} doesn't exist in the database.");
                    context.Result = new NotFoundResult();
                }
                else
                {
                    await next();
                }
            }
            else
            {
                await next();
            }
        }
    }
}
