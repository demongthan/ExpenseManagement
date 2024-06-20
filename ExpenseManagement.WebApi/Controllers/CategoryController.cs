using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common.ActionFilters;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.CategoriItem;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace ExpenseManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost("CreateCategory")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ApiReponse<ExpandoObject>> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto, [FromQuery] string? fileds)
        {
            var categoryEntity = _mapper.Map<CategoriesItem>(categoryCreateDto);

            return await _categoryService.CreateCategory(categoryEntity, fileds);
        }

        [HttpPut("UpdateCategory/{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateCategoryExistsAttribute))]
        public async Task<ApiReponse<ExpandoObject>> UpdateCategory([FromRoute] Guid id, [FromBody] CategoryUpdateDto categoryUpdateDto, string? fileds)
        {
            var categoryEntity = HttpContext.Items["category"] as CategoriesItem;

            _mapper.Map(categoryUpdateDto, categoryEntity);

            return await _categoryService.UpdateCategory(categoryEntity, fileds);
        }
    }
}
