using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common.ActionFilters;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Saving;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace ExpenseManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavingController : ControllerBase
    {
        private readonly ISavingService _savingService;
        private readonly IMapper _mapper;

        public SavingController(ISavingService savingService, IMapper mapper)
        {
            _mapper = mapper;
            _savingService = savingService;
        }

        [HttpPut("UpdateSaving/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateSavingExistsAttribute))]
        public async Task<ApiReponse<ExpandoObject>> UpdateSaving([FromRoute] Guid id, [FromBody] SavingUpdateDto savingUpdateDto, [FromQuery] string? fileds)
        {
            var savingEntity = HttpContext.Items["saving"] as Saving;

            _mapper.Map(savingUpdateDto, savingEntity);

            return await _savingService.UpdateSaving(savingEntity, fileds);
        }
    }
}
