using System.ComponentModel.DataAnnotations;

namespace ExpenseManagement.BusinessLogicLayer.DataDomains.Authentication
{
    public class TokenModelDto
    {
        [Required(ErrorMessage = "Code Role is a required field.")]
        public string? Token { get; set; }
        [Required(ErrorMessage = "Code Role is a required field.")]
        public string? RefreshToken { get; set; }
    }
}
