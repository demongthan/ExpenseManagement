using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common;
using ExpenseManagement.BusinessLogicLayer.Common.DataShaping.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.CategoriItem;
using ExpenseManagement.BusinessLogicLayer.DataDomains.DebtDetails;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using ExpenseManagement.DataAccessLayer.UnitOfWork.AstractClass;
using Microsoft.AspNetCore.Http;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly ISystemParameterRepository _parameterService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDataShaper<CategoryDto> _dataShaper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ILoggerManager loggerManager,
            IMapper mapper,
            ISystemParameterRepository systemParameterRepository,
            ICategoryRepository categoryRepository,
            IDataShaper<CategoryDto> dataShaper,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _loggerManager = loggerManager;
            _mapper = mapper;
            _parameterService = systemParameterRepository;
            _dataShaper = dataShaper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiReponse<ExpandoObject>> CreateCategory(CategoriesItem categoriesItemEntity, string? fileds)
        {
            categoriesItemEntity.UserCreate = "System";
            _categoryRepository.CreateCategory(categoriesItemEntity);

            if (await _unitOfWork.SaveChangesAsync())
            {
                var categoryReturn = _mapper.Map<CategoryDto>(categoriesItemEntity);
                var result = _dataShaper.ShapeData(categoryReturn, fileds);

                _loggerManager.LogInfo(string.Format("Category create successfully {0}", categoriesItemEntity.Name));
                var systemParameter = await _parameterService.GetSystemParameterAsynByCode(SystemParameterCode.CODE_CI_CREATESUCCESSFULL, false);

                return new ApiReponse<ExpandoObject>(true, systemParameter.Content, StatusCodes.Status200OK, result);
            }
            else
            {
                _loggerManager.LogError(string.Format("Debt Details create is failed {0}", categoriesItemEntity.Name));
                var systemParameter = await _parameterService.GetSystemParameterAsynByCode(SystemParameterCode.CODE_CI_CREATEFAIL, false);

                return new ApiReponse<ExpandoObject>(false, systemParameter.Content, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiReponse<ExpandoObject>> UpdateCategory(CategoriesItem categoriesItem, string? fileds)
        {
            categoriesItem.UpdatedDateUtc = DateTime.UtcNow;
            categoriesItem.UserUpdate = "System";
            _categoryRepository.UpdateCategory(categoriesItem);

            if (await _unitOfWork.SaveChangesAsync())
            {
                var categoryReturn = _mapper.Map<CategoryDto>(categoriesItem);
                var result = _dataShaper.ShapeData(categoryReturn, fileds);

                _loggerManager.LogInfo(string.Format("Debt Details update successfully {0}", categoriesItem.Name));
                var systemParameter = await _parameterService.GetSystemParameterAsynByCode(SystemParameterCode.CODE_CI_UPDATESUCCESSFULL, false);

                return new ApiReponse<ExpandoObject>(true, systemParameter.Content, StatusCodes.Status200OK, result);
            }
            else
            {
                _loggerManager.LogError(string.Format("Debt Details update is failed {0}", categoriesItem.Name));
                var systemParameter = await _parameterService.GetSystemParameterAsynByCode(SystemParameterCode.CODE_CI_UPDATEFAIL, false);

                return new ApiReponse<ExpandoObject>(false, systemParameter.Content, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
