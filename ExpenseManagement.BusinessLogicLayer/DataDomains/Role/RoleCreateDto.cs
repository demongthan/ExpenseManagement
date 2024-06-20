using System.ComponentModel.DataAnnotations;

namespace ExpenseManagement.BusinessLogicLayer.DataDomains.Role
{
    public class RoleCreateDto
    {
        [Required(ErrorMessage = "Code Role is a required field.")]
        [MaxLength(75, ErrorMessage = "Maximum length for the Code is 50 characters.")]
        public string Code { get; set; }
        public string? Description { get; set; }
    }
}
