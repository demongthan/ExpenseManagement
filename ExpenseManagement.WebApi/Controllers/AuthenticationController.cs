using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common.ActionFilters;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Authentication;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace ExpenseManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateUserExistsAttribute))]
        public async Task<ApiReponse<ExpandoObject>> CreateUser([FromBody] RegisterDto registerDto, [FromQuery] string? fileds)
        {
            return await _authenticationService.CreateUser(registerDto, fileds);
        }

        [HttpPost("Login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ApiReponse<ExpandoObject>> LoginUser([FromBody] LoginDto loginDto, [FromQuery] string? fileds)
        {
            return await _authenticationService.LoginUser(loginDto, fileds);
        }

        [HttpPost("RefreshToken")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ApiReponse<ExpandoObject>> RefreshToken([FromBody] TokenModelDto tokenModelDto, [FromQuery] string? fileds)
        {
            return await _authenticationService.RefreshToken(tokenModelDto, fileds);
        }

        [HttpPost("Revoke/{username}")]
        public async Task<ApiReponse<string>> Reveke([FromQuery] string username)
        {
            return await _authenticationService.Revoke(username);
        }
    }
}
