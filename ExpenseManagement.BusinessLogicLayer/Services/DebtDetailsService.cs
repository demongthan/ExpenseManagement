using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common;
using ExpenseManagement.BusinessLogicLayer.Common.DataShaping.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.DebtDetails;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using ExpenseManagement.DataAccessLayer.UnitOfWork.AstractClass;
using Microsoft.AspNetCore.Http;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services
{
    public class DebtDetailsService : IDebtDetailsService
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        private readonly IDataShaper<DebtDetailsDto> _dataShaper;
        private readonly IDebtDetailsRepository _debtDetailsRepository;
        private readonly ISystemParameterRepository _systemParameterRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DebtDetailsService(
            IMapper mapper,
            ILoggerManager loggerManager,
            IDataShaper<DebtDetailsDto> dataShaper,
            IDebtDetailsRepository debtDetailsRepository,
            ISystemParameterRepository systemParameterRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _loggerManager = loggerManager;
            _dataShaper = dataShaper;
            _debtDetailsRepository = debtDetailsRepository;
            _systemParameterRepository = systemParameterRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiReponse<ExpandoObject>> CreateDebtDetails(DebtDetails debtDetailsEntity, string fileds)
        {
            debtDetailsEntity.UserCreate = "System";
            _debtDetailsRepository.CreateDebt(debtDetailsEntity);

            if (await _unitOfWork.SaveChangesAsync())
            {
                var debtDetailsReturn = _mapper.Map<DebtDetailsDto>(debtDetailsEntity);
                var result = _dataShaper.ShapeData(debtDetailsReturn, fileds);

                _loggerManager.LogInfo(string.Format("Debt Details create successfully {0}", debtDetailsEntity.Name));
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_DD_CREATESUCCESSFULL, false);

                return new ApiReponse<ExpandoObject>(true, systemParameter.Content, StatusCodes.Status200OK, result);
            }
            else
            {
                _loggerManager.LogError(string.Format("Debt Details create is failed {0}", debtDetailsEntity.Name));
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_DD_CREATEFAIL, false);

                return new ApiReponse<ExpandoObject>(false, systemParameter.Content, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiReponse<ExpandoObject>> UpdateDebtDetails(DebtDetails debtDetailsEntity, string fileds)
        {
            debtDetailsEntity.UserUpdate = "System";
            debtDetailsEntity.UpdatedDateUtc = DateTime.UtcNow;

            _debtDetailsRepository.UpdateDebt(debtDetailsEntity);

            if (await _unitOfWork.SaveChangesAsync())
            {
                var debtDetailsReturn = _mapper.Map<DebtDetailsDto>(debtDetailsEntity);
                var result = _dataShaper.ShapeData(debtDetailsReturn, fileds);

                _loggerManager.LogInfo(string.Format("Debt Details update successfully {0}", debtDetailsEntity.Name));
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_DD_UPDATESUCCESSFULL, false);

                return new ApiReponse<ExpandoObject>(true, systemParameter.Content, StatusCodes.Status200OK, result);
            }
            else
            {
                _loggerManager.LogError(string.Format("Debt Details update is failed {0}", debtDetailsEntity.Name));
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_DD_UPDATEFAIL, false);

                return new ApiReponse<ExpandoObject>(false, systemParameter.Content, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
