using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common.ActionFilters;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.ItemDailySpending;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Saving;
using ExpenseManagement.BusinessLogicLayer.Services;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace ExpenseManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemDailySpendingController : ControllerBase
    {
        private readonly IItemDailySpendingService _itemDailySpendingService;
        private readonly IMapper _mapper;
        public ItemDailySpendingController(IItemDailySpendingService itemDailySpendingService, IMapper mapper)
        {
            _itemDailySpendingService = itemDailySpendingService;
            _mapper = mapper;
        }

        [HttpPost("CreateItemDailySpending")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ApiReponse<ExpandoObject>> CreateRole([FromBody] ItemDailySpendingCreateDto itemDailySpendingCreateDto, [FromQuery] string? fileds)
        {
            var itemDailySpendingEntity = _mapper.Map<ItemDailySpending>(itemDailySpendingCreateDto);

            return await _itemDailySpendingService.CreateItemDailySpending(itemDailySpendingEntity, fileds);
        }

        [HttpPut("UpdateItemDailySpending/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateItemDailySpendingExistsAttribute))]
        public async Task<ApiReponse<ExpandoObject>> UpdateSaving([FromRoute] Guid id, [FromBody] ItemDailySpendingUpdateDto itemDailySpendingUpdateDto, [FromQuery] string? fileds)
        {
            var itemDailySpendingEntity = HttpContext.Items["itemDailySpending"] as ItemDailySpending;

            _mapper.Map(itemDailySpendingUpdateDto, itemDailySpendingEntity);

            return await _itemDailySpendingService.UpdateItemDailySpending(itemDailySpendingEntity, fileds);
        }

    }
}
