using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common;
using ExpenseManagement.BusinessLogicLayer.Common.DataShaping.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Saving;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using ExpenseManagement.DataAccessLayer.UnitOfWork.AstractClass;
using Microsoft.AspNetCore.Http;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services
{
    public class SavingService : ISavingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISavingRepository _savingRepository;
        private readonly IDataShaper<SavingDto> _dataShaper;
        private readonly ILoggerManager _loggerManager;
        private readonly ISystemParameterRepository _systemParameterRepository;

        public SavingService(IUnitOfWork unitOfWork,
            IMapper mapper,
            ISavingRepository savingRepository,
            IDataShaper<SavingDto> dataShaper,
            ILoggerManager loggerManager,
            ISystemParameterRepository systemParameterRepository
            )
        {
            _dataShaper = dataShaper;
            _loggerManager = loggerManager;
            _mapper = mapper;
            _savingRepository = savingRepository;
            _systemParameterRepository = systemParameterRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiReponse<ExpandoObject>> UpdateSaving(Saving saving, string fileds)
        {
            saving.UserUpdate = "System";
            saving.UpdatedDateUtc = DateTime.UtcNow;
            saving.SavingsAvailable = saving.CashSaving + saving.CreditSaving;
            saving.UltimateSavings = saving.SavingsAvailable - saving.Debts;

            _savingRepository.UpdateSaving(saving);

            if (await _unitOfWork.SaveChangesAsync())
            {
                var savingReturn = _mapper.Map<SavingDto>(saving);
                var result = _dataShaper.ShapeData(savingReturn, fileds);
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_SAVING_UPDATESUCCESS, false);

                _loggerManager.LogInfo(string.Format("Update Saving with id {0} successfully", saving.Id));

                return new ApiReponse<ExpandoObject>(true, systemParameter.Content, StatusCodes.Status200OK, result);
            }
            else
            {
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_SAVING_UPDATEFAIL, false);

                _loggerManager.LogError(string.Format("Update Saving with id {0} falied", saving.Id));

                return new ApiReponse<ExpandoObject>(true, systemParameter.Content, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
