using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.LoggerService;
using ExpenseManagement.DataAccessLayer.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using ExpenseManagement.BusinessLogicLayer.Common.AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common.DataShaping.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.DataShaping;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Authentication;
using ExpenseManagement.DataAccessLayer.UnitOfWork.AstractClass;
using ExpenseManagement.DataAccessLayer.UnitOfWork;
using ExpenseManagement.BusinessLogicLayer.Common.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Services;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using ExpenseManagement.DataAccessLayer.Repository;
using ExpenseManagement.BusinessLogicLayer.DataDomains.SystemParameter;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Saving;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.BusinessLogicLayer.DataDomains.ItemDailySpending;
using ExpenseManagement.BusinessLogicLayer.DataDomains.DebtDetails;
using ExpenseManagement.BusinessLogicLayer.DataDomains.RevenueDetails;
using ExpenseManagement.BusinessLogicLayer.DataDomains.CategoriItem;

namespace ExpenseManagement.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this WebApplicationBuilder builder)
            => builder.Services.AddDbContext<DataDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        public static void ConfigureIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<DataDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            });
        }

        public static void ConfigureAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
        }

        public static void ConfigureLoggerService(this WebApplicationBuilder builder)
            => builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureDIAutoMapper(this WebApplicationBuilder builder)
            => builder.Services.AddAutoMapper(typeof(MappingProfile));

        public static void ConfigureDIDataShaper(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IDataShaper<RegisterDto>, DataShaper<RegisterDto>>();
            builder.Services.AddScoped<IDataShaper<SystemParameterDto>, DataShaper<SystemParameterDto>>();
            builder.Services.AddScoped<IDataShaper<RoleDto>, DataShaper<RoleDto>>();
            builder.Services.AddScoped<IDataShaper<TokenModelDto>, DataShaper<TokenModelDto>>();
            builder.Services.AddScoped<IDataShaper<SavingDto>, DataShaper<SavingDto>>();
            builder.Services.AddScoped<IDataShaper<ItemDailySpendingDto>, DataShaper<ItemDailySpendingDto>>();
            builder.Services.AddScoped<IDataShaper<DebtDetailsDto>, DataShaper<DebtDetailsDto>>();
            builder.Services.AddScoped<IDataShaper<RevenueDetailsDto>, DataShaper<RevenueDetailsDto>>();
            builder.Services.AddScoped<IDataShaper<CategoryDto>, DataShaper<CategoryDto>>();
        }

        public static void ConfigureDIRepsitory(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISystemParameterRepository, SystemParameterRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<ILoginModelRepository, LoginModelRepository>();
            builder.Services.AddScoped<ISavingRepository, SavingRepository>();
            builder.Services.AddScoped<IItemDailySpendingRepository, ItemDailySpendingRepository>();
            builder.Services.AddScoped<IDebtDetailsRepository, DebtDetailsRepository>();
            builder.Services.AddScoped<IRevenueDetailRepository, RevenueDetailRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        }

        public static void ConfigureDIActionFilters(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ValidationFilterAttribute>();
            builder.Services.AddScoped<ValidateSystemParameterExistsAttribute>();
            builder.Services.AddScoped<ValidateRoleExistsAttribute>();
            builder.Services.AddScoped<ValidateUserExistsAttribute>();
            builder.Services.AddScoped<ValidateSavingExistsAttribute>();
            builder.Services.AddScoped<ValidateItemDailySpendingExistsAttribute>();
            builder.Services.AddScoped<ValidateDebtDetailsExistsAttribute>();
            builder.Services.AddScoped<ValidateRevenueDetailsExistsAttribute>();
            builder.Services.AddScoped<ValidateCategoryExistsAttribute>();
        }

        public static void ConfigureDIModelState(this WebApplicationBuilder builder) =>
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        public static void ConfigureDIService(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISystemParameterService, SystemParameterService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ISavingService, SavingService>();
            builder.Services.AddScoped<IItemDailySpendingService, ItemDailySpendingService>();
            builder.Services.AddScoped<IDebtDetailsService, DebtDetailsService>();
            builder.Services.AddScoped<IRevenueDetailsService, RevenueDetailsService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}