using AutoMapper;
using ExpenseManagement.BusinessLogicLayer.Common;
using ExpenseManagement.BusinessLogicLayer.Common.DataShaping.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.LoggerService.AstractClass;
using ExpenseManagement.BusinessLogicLayer.Common.Reponse;
using ExpenseManagement.BusinessLogicLayer.DataDomains.Authentication;
using ExpenseManagement.BusinessLogicLayer.Services.AstractClass;
using ExpenseManagement.DataAccessLayer.DataModels;
using ExpenseManagement.DataAccessLayer.Repository.AstractClass;
using ExpenseManagement.DataAccessLayer.UnitOfWork.AstractClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExpenseManagement.BusinessLogicLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _loggerManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDataShaper<RegisterDto> _dataShaper;
        private readonly IDataShaper<TokenModelDto> _tokenModelShaper;
        private readonly ISystemParameterRepository _systemParameterRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;
        private readonly ILoginModelRepository _loginModelRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(ILoggerManager loggerManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IDataShaper<RegisterDto> dataShaper,
            ISystemParameterRepository systemParameterRepository,
            IRoleRepository roleRepository,
            IConfiguration configuration,
            ILoginModelRepository loginModelRepository,
            IDataShaper<TokenModelDto> tokenModelShaper,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _loggerManager = loggerManager;
            _systemParameterRepository = systemParameterRepository;
            _roleRepository = roleRepository;
            _dataShaper = dataShaper;
            _configuration = configuration;
            _loginModelRepository = loginModelRepository;
            _tokenModelShaper = tokenModelShaper;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiReponse<ExpandoObject>> CreateUser(RegisterDto registerDto, string fileds)
        {
            IdentityUser user = new()
            {
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.Username
            };

            var userCreate = await _userManager.CreateAsync(user, registerDto.Password);
            if (!userCreate.Succeeded)
            {
                var systemParameterFail = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_CREATEUSERFAIL, false);

                _loggerManager.LogError("Register: User creation failed! Server Error");
                return new ApiReponse<ExpandoObject>(false, systemParameterFail == null ? "" : systemParameterFail.Content, StatusCodes.Status500InternalServerError);
            }

            var role = await _roleRepository.GetRoleAsynByCode(SystemParameterCode.ROLEDEFAULTCREATEUSER, false);
            if (role != null)
            {
                if (await _roleManager.RoleExistsAsync(role.Code))
                {
                    await _userManager.AddToRoleAsync(user, role.Code);
                }
            }

            var result = _dataShaper.ShapeData(registerDto, fileds);
            var systemParameterSuccessfull = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_CREATEROLESUCCESFULL, false);

            _loggerManager.LogInfo("Register: User created successfully! " + registerDto.Username);
            return new ApiReponse<ExpandoObject>(true, systemParameterSuccessfull == null ? "" : systemParameterSuccessfull.Content, StatusCodes.Status200OK, result);
        }

        public async Task<ApiReponse<ExpandoObject>> LoginUser(LoginDto loginDto, string fileds)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                string stringToken = await GetTokenAsync(user);
                string refreshToken = GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                DateTime refreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                var loginModel = new LoginModel(user.Id, stringToken, refreshToken, refreshTokenExpiryTime);
                var tokenModelDto = _mapper.Map<TokenModelDto>(loginModel);

                _loginModelRepository.CreateLoginModelAsyn(loginModel);

                var systemParameterSuccessfull = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_LOGINSUCCESSFULL, false);
                var result = _tokenModelShaper.ShapeData(tokenModelDto, fileds);

                return new ApiReponse<ExpandoObject>(false, systemParameterSuccessfull == null ? "" : systemParameterSuccessfull.Content, StatusCodes.Status200OK, result);
            }
            else
            {
                var systemParameterFail = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_LOGINFAIL, false);
                return new ApiReponse<ExpandoObject>(false, systemParameterFail == null ? "" : systemParameterFail.Content, StatusCodes.Status400BadRequest);
            }
        }

        public async Task<ApiReponse<ExpandoObject>> RefreshToken(TokenModelDto tokenModelDto, string fileds)
        {
            string accessToken = tokenModelDto.Token;
            string refreshToken = tokenModelDto.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(username);
            var loginModel = await _loginModelRepository.GetLoginModelAsyn(user.Id, false);

            if (user is null || loginModel.RefreshToken != refreshToken || loginModel.RefreshTokenExpiryTime <= DateTime.Now)
            {
                _loggerManager.LogError("REFRESH TOKEN: user name not exist, refresh not equals, refresh token unexpired");

                var systemParameterFail = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_REFRESHTOKENFAIL, false);
                return new ApiReponse<ExpandoObject>(false, systemParameterFail == null ? "" : systemParameterFail.Content, StatusCodes.Status400BadRequest);
            }

            var newAccessToken = await GetTokenAsync(user);
            var newRefreshToken = GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
            DateTime refreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
            loginModel.RefreshToken = newRefreshToken;
            loginModel.Token = newAccessToken;
            loginModel.RefreshTokenExpiryTime = refreshTokenExpiryTime;

            _loginModelRepository.UpdateLoginModelAsyn(loginModel);

            if (await _unitOfWork.SaveChangesAsync())
            {
                _loggerManager.LogInfo(String.Format("REFRESH TOKEN: Resfresh token of User name: {0} at {1})", user.UserName, DateTime.Now));

                var systemParameterSuccessfull = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_CREATEROLESUCCESFULL, false);
                var result = _tokenModelShaper.ShapeData(tokenModelDto, fileds);

                return new ApiReponse<ExpandoObject>(false, systemParameterSuccessfull == null ? "" : systemParameterSuccessfull.Content, StatusCodes.Status200OK, result);
            }
            else
            {
                _loggerManager.LogError("REFRESH TOKEN: server error");

                var systemParameterFail = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_REFRESHTOKENFAIL, false);
                return new ApiReponse<ExpandoObject>(false, systemParameterFail == null ? "" : systemParameterFail.Content, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ApiReponse<string>> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                _loggerManager.LogError(String.Format("Revoke: user name {0} not exist", username));

                var systemParameterFail = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_REVOKEFAIL, false);
                return new ApiReponse<string>(false, systemParameterFail == null ? "" : systemParameterFail.Content, StatusCodes.Status400BadRequest);
            }

            var loginModel = await _loginModelRepository.GetLoginModelAsyn(user.Id, false);

            loginModel.RefreshToken = null;
            _loginModelRepository.UpdateLoginModelAsyn(loginModel);

            if (await _unitOfWork.SaveChangesAsync())
            {
                _loggerManager.LogInfo(String.Format("REVOKE: Resfresh token of User name: {0} at {1})", user.UserName, DateTime.Now));

                var systemParameterSuccessfull = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_REVOKESUCCESSFULL, false);
                return new ApiReponse<string>(false, systemParameterSuccessfull == null ? "" : systemParameterSuccessfull.Content, StatusCodes.Status200OK);
            }
            else
            {
                _loggerManager.LogError("REVOKE: server error");

                var systemParameterFail = await _systemParameterRepository.GetSystemParameterAsynByCode(SystemParameterCode.CODE_REVOKEFAIL, false);
                return new ApiReponse<string>(false, systemParameterFail == null ? "" : systemParameterFail.Content, StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<string> GetTokenAsync(IdentityUser user)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var userRoles = await _userManager.GetRolesAsync(user);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, string.Join(',',userRoles))
                    }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
