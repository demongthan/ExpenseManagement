using System.ComponentModel.DataAnnotations;

namespace ExpenseManagement.BusinessLogicLayer.DataDomains.Authentication
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Code Role is a required field.")]
        public string Username { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required(ErrorMessage = "Email is a required field.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is a required field.")]
        public string Password { get; set; }
    }
}
