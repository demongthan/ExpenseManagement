using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common;
using ExpenseManagement.BusinessLogicLayer.Common.DataShaping.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.ItemDailySpending;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Saving;
using ExpenseManagement.BusinessLogicLayer.DataDomains.SystemParameter;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using ExpenseManagement.DataAccessLayer.UnitOfWork.AstractClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services
{
    public class ItemDailySpendingService : IItemDailySpendingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IItemDailySpendingRepository _itemDailySpendingRepository;
        private readonly IDataShaper<ItemDailySpendingDto> _dataShaper;
        private readonly ILoggerManager _loggerManager;
        private readonly ISystemParameterRepository _systemParameterRepository;

        public ItemDailySpendingService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IItemDailySpendingRepository itemDailySpendingRepository,
            IDataShaper<ItemDailySpendingDto> dataShaper,
            ILoggerManager loggerManager,
            ISystemParameterRepository systemParameterRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _itemDailySpendingRepository = itemDailySpendingRepository;
            _dataShaper = dataShaper;
            _loggerManager = loggerManager;
            _systemParameterRepository = systemParameterRepository;
        }

        public async Task<ApiReponse<ExpandoObject>> CreateItemDailySpending(ItemDailySpending itemDailySpending, string fileds)
        {
            itemDailySpending.UserCreate = "System";
            _itemDailySpendingRepository.CreateItemDailySpending(itemDailySpending);

            if (await _unitOfWork.SaveChangesAsync())
            {
                var itemDailySpendingReturn = _mapper.Map<ItemDailySpendingDto>(itemDailySpending);
                var result = _dataShaper.ShapeData(itemDailySpendingReturn, fileds);
                _loggerManager.LogError(string.Format("System Parameter create successfully {0}", itemDailySpending.Name));
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_IDS_CREATESUCCESSFULL, false);

                return new ApiReponse<ExpandoObject>(true, systemParameter.Content, StatusCodes.Status200OK, result);
            }
            else
            {
                _loggerManager.LogError(string.Format("System Parameter create is failed {0}", itemDailySpending.Name));
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_IDS_CREATEFAIL, false);

                return new ApiReponse<ExpandoObject>(false, systemParameter.Content, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiReponse<ExpandoObject>> UpdateItemDailySpending(ItemDailySpending itemDailySpending, string fileds)
        {
            itemDailySpending.UserUpdate = "System";
            _itemDailySpendingRepository.UpdateItemDailySpending(itemDailySpending);

            if (await _unitOfWork.SaveChangesAsync())
            {
                var itemDailySpendingReturn = _mapper.Map<ItemDailySpendingDto>(itemDailySpending);
                var result = _dataShaper.ShapeData(itemDailySpendingReturn, fileds);
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_IDS_UPDATESUCCESSFULL, false);

                _loggerManager.LogInfo(string.Format("Update Item Daily Spending with id {0} successfully", itemDailySpending.Id));

                return new ApiReponse<ExpandoObject>(true, systemParameter.Content, StatusCodes.Status200OK, result);
            }
            else
            {
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_IDS_UPDATEFAIL, false);

                _loggerManager.LogError(string.Format("Update Saving with id {0} falied", itemDailySpending.Id));

                return new ApiReponse<ExpandoObject>(true, systemParameter.Content, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
