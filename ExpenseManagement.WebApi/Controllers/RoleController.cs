using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common.ActionFilters;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Role;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace ExpenseManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _mapper = mapper;
            _roleService = roleService;
        }

        [HttpPost("CreateRole")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateRoleExistsAttribute))]
        public async Task<ApiReponse<ExpandoObject>> CreateRole([FromBody] RoleCreateDto roleCreateDto, [FromQuery] string? fileds)
        {
            var roleEntity = _mapper.Map<Role>(roleCreateDto);

            return await _roleService.CreateRole(roleEntity, fileds);
        }
    }
}
