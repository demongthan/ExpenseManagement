using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using ExpenseManagement.BusinessLogicLayer.DataDomains.SystemParameter;

namespace ExpenseManagement.BusinessLogicLayer.Common.ActionFilters
{
    public class ValidateSystemParameterExistsAttribute : IAsyncActionFilter
    {
        private readonly ISystemParameterRepository _systemParameterRepository;
        private readonly ILoggerManager _loggerManager;

        public ValidateSystemParameterExistsAttribute(ISystemParameterRepository systemParameterRepository, ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
            _systemParameterRepository = systemParameterRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;

            if (method.Equals("POST"))
            {
                var param = (SystemParameterCreateDto)context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(param.Code, true);

                if (systemParameter != null)
                {
                    _loggerManager.LogInfo($"System Parameter code with code: {param.Code} doesn't exist in the database.");
                    context.Result = new NotFoundResult();
                }
                else
                {
                    await next();
                }
            }
            else
            {
                var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
                var id = (Guid)context.ActionArguments["id"];
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsyn(id, trackChanges);

                if (systemParameter == null)
                {
                    _loggerManager.LogInfo($"System Parameter with id: {id} doesn't exist in the database.");
                    context.Result = new NotFoundResult();
                }
                else
                {
                    context.HttpContext.Items.Add("systemParameter", systemParameter);
                    await next();
                }
            }
        }
    }
}
