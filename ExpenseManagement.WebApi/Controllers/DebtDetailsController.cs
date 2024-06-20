using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common.ActionFilters;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.DebtDetails;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace ExpenseManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtDetailsController : ControllerBase
    {
        private readonly IDebtDetailsService _debtDetailsService;
        private readonly IMapper _mapper;

        public DebtDetailsController(IDebtDetailsService debtDetailsService, IMapper mapper)
        {
            _debtDetailsService = debtDetailsService;
            _mapper = mapper;
        }

        [HttpPost("CreateDebtDetails")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ApiReponse<ExpandoObject>> CreateDebt([FromBody] DebtDetailsCreateDto debtDetailsCreateDto, [FromQuery] string? fileds)
        {
            var debtDetailEntity = _mapper.Map<DebtDetails>(debtDetailsCreateDto);

            return await _debtDetailsService.CreateDebtDetails(debtDetailEntity, fileds);
        }

        [HttpPut("UpdateDebtDetails/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateDebtDetailsExistsAttribute))]
        public async Task<ApiReponse<ExpandoObject>> UpdateDebtDetails([FromRoute] Guid id, [FromBody] DebtDetailsUpdateDto debtDetailsUpdateDto, [FromQuery] string? fileds)
        {
            var debtDailsEntity = HttpContext.Items["debtDetails"] as DebtDetails;

            _mapper.Map(debtDetailsUpdateDto, debtDailsEntity);

            return await _debtDetailsService.UpdateDebtDetails(debtDailsEntity, fileds);
        }
    }
}
