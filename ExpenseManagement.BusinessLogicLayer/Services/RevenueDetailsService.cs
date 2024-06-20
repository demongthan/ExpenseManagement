using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common;
using ExpenseManagement.BusinessLogicLayer.Common.DataShaping.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.DebtDetails;
using ExpenseManagement.BusinessLogicLayer.DataDomains.RevenueDetails;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using ExpenseManagement.DataAccessLayer.UnitOfWork.AstractClass;
using Microsoft.AspNetCore.Http;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services
{
    public class RevenueDetailsService : IRevenueDetailsService
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        private readonly IDataShaper<RevenueDetailsDto> _dataShaper;
        private readonly IRevenueDetailRepository _revenueDetailRepository;
        private readonly ISystemParameterRepository _systemParameterRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RevenueDetailsService(IMapper mapper,
            ILoggerManager loggerManager,
            IDataShaper<RevenueDetailsDto> dataShaper,
            IRevenueDetailRepository revenueDetailRepository,
            ISystemParameterRepository systemParameterRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _loggerManager = loggerManager;
            _dataShaper = dataShaper;
            _revenueDetailRepository = revenueDetailRepository;
            _systemParameterRepository = systemParameterRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiReponse<ExpandoObject>> CreateRevenue(RevenueDetail revenueDetail, string? fileds)
        {
            revenueDetail.UserCreate = "System";
            _revenueDetailRepository.CreateRevenue(revenueDetail);

            if (await _unitOfWork.SaveChangesAsync())
            {
                var revenueDetailReturn = _mapper.Map<RevenueDetailsDto>(revenueDetail);
                var result = _dataShaper.ShapeData(revenueDetailReturn, fileds);

                _loggerManager.LogInfo(string.Format("Debt Details create successfully {0}", revenueDetail.Name));
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_RD_CREATESUCCESSFULL, false);

                return new ApiReponse<ExpandoObject>(true, systemParameter.Content, StatusCodes.Status200OK, result);
            }
            else
            {
                _loggerManager.LogError(string.Format("Debt Details create is failed {0}", revenueDetail.Name));
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_RD_CREATEFAIL, false);

                return new ApiReponse<ExpandoObject>(false, systemParameter.Content, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiReponse<ExpandoObject>> UpdateRevenue(RevenueDetail revenueDetail, string? fileds)
        {
            revenueDetail.UpdatedDateUtc = DateTime.UtcNow;
            revenueDetail.UserUpdate = "System";
            _revenueDetailRepository.UpdateRevenue(revenueDetail);

            if (await _unitOfWork.SaveChangesAsync())
            {
                var revenueDetailReturn = _mapper.Map<RevenueDetailsDto>(revenueDetail);
                var result = _dataShaper.ShapeData(revenueDetailReturn, fileds);

                _loggerManager.LogInfo(string.Format("Debt Details update successfully {0}", revenueDetail.Name));
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_RD_UPDATESUCCESSFULL, false);

                return new ApiReponse<ExpandoObject>(true, systemParameter.Content, StatusCodes.Status200OK, result);
            }
            else
            {
                _loggerManager.LogError(string.Format("Debt Details update is failed {0}", revenueDetail.Name));
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_RD_UPDATEFAIL, false);

                return new ApiReponse<ExpandoObject>(false, systemParameter.Content, StatusCodes.Status500InternalServerError);
            }

        }
    }
}
