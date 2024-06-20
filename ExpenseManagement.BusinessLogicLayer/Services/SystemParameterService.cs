using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common.DataShaping.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.SystemParameter;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using ExpenseManagement.DataAccessLayer.UnitOfWork.AstractClass;
using Microsoft.AspNetCore.Http;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services
{
    public class SystemParameterService: ISystemParameterService
    {
        private readonly ISystemParameterRepository _systemParameterService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDataShaper<SystemParameterDto> _dataShaper;
        private readonly ILoggerManager _loggerManager;

        public SystemParameterService(
            ISystemParameterRepository systemParameterRepository, 
            IUnitOfWork unitOfWork,
            IDataShaper<SystemParameterDto> dataShaper,
            IMapper mapper, 
            ILoggerManager loggerManager) 
        {
            _dataShaper = dataShaper;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggerManager = loggerManager;
            _systemParameterService = systemParameterRepository;
        }

        public async Task<ApiReponse<ExpandoObject>> CreateSystemParameter(SystemParameter systemParameterEntity, string fileds)
        {
            systemParameterEntity.UserCreate = "System";
            _systemParameterService.CreateSystemParameterAsyn(systemParameterEntity);

            if (await _unitOfWork.SaveChangesAsync())
            {
                var systemParameterReturn = _mapper.Map<SystemParameterDto>(systemParameterEntity);
                var result = _dataShaper.ShapeData(systemParameterReturn, fileds);
                _loggerManager.LogError(string.Format("System Parameter create successfully {0}", systemParameterEntity.Code));
                return new ApiReponse<ExpandoObject>(true, "successfully", StatusCodes.Status200OK, result);
            }
            else
            {
                _loggerManager.LogError(string.Format("System Parameter create is failed {0}", systemParameterEntity.Code));
                return new ApiReponse<ExpandoObject>(false, "Failed", StatusCodes.Status500InternalServerError);
            }
        }
    }
}
