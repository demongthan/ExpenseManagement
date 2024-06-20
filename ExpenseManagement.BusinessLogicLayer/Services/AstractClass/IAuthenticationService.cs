using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Authentication;
using System.Dynamic;

namespace ExpenseManagement.BusinessLogicLayer.Services.AstractClass
{
    public interface IAuthenticationService
    {
        Task<ApiReponse<ExpandoObject>> CreateUser(RegisterDto registerDto, string fileds);
        Task<ApiReponse<ExpandoObject>> LoginUser(LoginDto loginDto, string fileds);
        Task<ApiReponse<ExpandoObject>> RefreshToken(TokenModelDto tokenModelDto, string fileds);
        Task<ApiReponse<string>> Revoke(string username);
    }
}
