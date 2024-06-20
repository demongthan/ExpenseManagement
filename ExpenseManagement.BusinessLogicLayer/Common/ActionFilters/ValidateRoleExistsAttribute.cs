using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Role;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExpenseManagement.BusinessLogicLayer.Common.ActionFilters
{
    public class ValidateRoleExistsAttribute : IAsyncActionFilter
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IRoleRepository _roleRepository;

        public ValidateRoleExistsAttribute(ILoggerManager loggerManager, IRoleRepository roleRepository)
        {
            _loggerManager = loggerManager;
            _roleRepository = roleRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            if (method.Equals("POST"))
            {
                var param = (RoleCreateDto)context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
                var roleExists = await _roleRepository.GetRoleAsynByCode(param.Code, false);

                if (roleExists != null)
                {
                    _loggerManager.LogError($"Role Code with User Name: {param.Code} exist in the database.");
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
