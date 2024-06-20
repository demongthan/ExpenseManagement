using ExpenseManagement.DataAccessLayer.DataModels.Common;

namespace ExpenseManagement.DataAccessLayer.DataModels
{
    public class LoginModel : DateTimeCommon
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Account User { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public LoginModel(string userId, string token, string refreshToken, DateTime refreshTokenExpiryTime)
        {
            UserId = userId;
            Token = token;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }
    }
}
