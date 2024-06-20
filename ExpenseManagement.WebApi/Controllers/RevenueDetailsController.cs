using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common.ActionFilters;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.RevenueDetails;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace ExpenseManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueDetailsController : ControllerBase
    {
        private readonly IRevenueDetailsService _revenueDetailsService;
        private readonly IMapper _mapper;
        public RevenueDetailsController(IRevenueDetailsService revenueDetailsService, IMapper mapper)
        {
            _mapper = mapper;
            _revenueDetailsService = revenueDetailsService;
        }

        [HttpPost("CreateRevenueDetails")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ApiReponse<ExpandoObject>> CreateRevenue([FromBody] RevenueDetailCreateDto revenueDetailCreateDto, [FromQuery] string? fileds)
        {
            var revenueDetailEntity = _mapper.Map<RevenueDetail>(revenueDetailCreateDto);

            return await _revenueDetailsService.CreateRevenue(revenueDetailEntity, fileds);
        }

        [HttpPut("UpdateRevenueDetails/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateRevenueDetailsExistsAttribute))]
        public async Task<ApiReponse<ExpandoObject>> UpdateRevenueDetails([FromRoute] Guid id, [FromBody] RevenueDetailUpdateDto revenueDetailUpdateDto, [FromQuery] string? fileds)
        {
            var revenueDetailEnity = HttpContext.Items["revenueDetail"] as RevenueDetail;

            _mapper.Map(revenueDetailUpdateDto, revenueDetailEnity);

            return await _revenueDetailsService.UpdateRevenue(revenueDetailEnity, fileds);
        }
    }
}
