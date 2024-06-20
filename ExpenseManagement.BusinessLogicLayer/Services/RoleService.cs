using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common;
using ExpenseManagement.BusinessLogicLayer.Common.DataShaping.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Role;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using ExpenseManagement.DataAccessLayer.UnitOfWork.AstractClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDataShaper<RoleDto> _dataShaper;
        private readonly ILoggerManager _loggerManager;
        private readonly ISystemParameterRepository _systemParameterRepository;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDataShaper<RoleDto> dataShaper,
            ILoggerManager loggerManager,
            ISystemParameterRepository systemParameterRepository,
            RoleManager<IdentityRole> roleManager)
        {
            _dataShaper = dataShaper;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _loggerManager = loggerManager;
            _unitOfWork = unitOfWork;
            _systemParameterRepository = systemParameterRepository;
            _roleManager = roleManager;
        }

        public async Task<ApiReponse<ExpandoObject>> CreateRole(Role roleEntity, string fileds)
        {
            roleEntity.UserCreate = "System";
            _roleRepository.CreateRoleAsyn(roleEntity);

            if (await _unitOfWork.SaveChangesAsync())
            {
                if (!await _roleManager.RoleExistsAsync(roleEntity.Code))
                    await _roleManager.CreateAsync(new IdentityRole(roleEntity.Code));

                var roleReturn = _mapper.Map<RoleDto>(roleEntity);
                var result = _dataShaper.ShapeData(roleReturn, fileds);
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_CREATEROLESUCCESFULL, false);

                _loggerManager.LogError(string.Format("Role create successfully {0}", roleReturn.Code));
                return new ApiReponse<ExpandoObject>(true, systemParameter == null ? "" : systemParameter.Content, StatusCodes.Status200OK, result);
            }
            else
            {
                var systemParameter = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_CREATEROLEFAIL, false);

                _loggerManager.LogError(string.Format("Role create is failed {0}", roleEntity.Code));
                return new ApiReponse<ExpandoObject>(false, systemParameter == null ? "" : systemParameter.Content, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
