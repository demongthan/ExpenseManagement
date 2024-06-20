using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common.ActionFilters;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.SystemParameter;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace ExpenseManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemParameterController : ControllerBase
    {
        private readonly ISystemParameterService _systemParameterService;
        private readonly IMapper _mapper;

        public SystemParameterController(ISystemParameterService systemParameterService, IMapper mapper)
        {
            _mapper = mapper;
            _systemParameterService = systemParameterService;
        }

        [HttpPost("CreateSystemParameter")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateSystemParameterExistsAttribute))]
        public async Task<ApiReponse<ExpandoObject>> CreateSystemParameter([FromBody] SystemParameterCreateDto systemParameterCreateDto, [FromQuery] string? fileds)
        {
            var systemParameterEntity = _mapper.Map<SystemParameter>(systemParameterCreateDto);

            return await _systemParameterService.CreateSystemParameter(systemParameterEntity, fileds);
        }
    }
}
