using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Authentication;
using ExpenseManagement.BusinessLogicLayer.DataDomains.CategoriItem;
using ExpenseManagement.BusinessLogicLayer.DataDomains.DebtDetails;
using ExpenseManagement.BusinessLogicLayer.DataDomains.ItemDailySpending;
using ExpenseManagement.BusinessLogicLayer.DataDomains.RevenueDetails;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Role;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Saving;
using ExpenseManagement.BusinessLogicLayer.DataDomains.SystemParameter;
using ExpenseManagement.DataAccessLayer.DataModels;

namespace ExpenseManagement.BusinessLogicLayer.Common.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, RegisterDto>().ReverseMap();

            CreateMap<SystemParameter, SystemParameterDto>().ReverseMap();
            CreateMap<SystemParameterCreateDto, SystemParameter>().ReverseMap();

            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<RoleCreateDto, Role>().ReverseMap();

            CreateMap<LoginModel, TokenModelDto>().ReverseMap();

            CreateMap<Saving, SavingDto>().ReverseMap();
            CreateMap<SavingUpdateDto, Saving>().ReverseMap();

            CreateMap<ItemDailySpending, ItemDailySpendingDto>().ReverseMap();
            CreateMap<ItemDailySpendingCreateDto, ItemDailySpending>().ReverseMap();
            CreateMap<ItemDailySpendingUpdateDto, ItemDailySpending>().ReverseMap();

            CreateMap<DebtDetails, DebtDetailsDto>().ReverseMap();
            CreateMap<DebtDetailsCreateDto, DebtDetails>().ReverseMap();
            CreateMap<DebtDetailsUpdateDto, DebtDetails>().ReverseMap();

            CreateMap<RevenueDetail, RevenueDetailsDto>().ReverseMap();
            CreateMap<RevenueDetailCreateDto, RevenueDetail>().ReverseMap();
            CreateMap<RevenueDetailUpdateDto, RevenueDetail>().ReverseMap();

            CreateMap<CategoriesItem, CategoryDto>().ReverseMap();
            CreateMap<CategoryCreateDto, CategoriesItem>().ReverseMap();
            CreateMap<CategoryUpdateDto, CategoriesItem>().ReverseMap();
        }
    }
}
