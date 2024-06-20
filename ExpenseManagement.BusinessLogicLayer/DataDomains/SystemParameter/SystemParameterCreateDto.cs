using System.ComponentModel.DataAnnotations;

namespace ExpenseManagement.BusinessLogicLayer.DataDomains.SystemParameter
{
    public class SystemParameterCreateDto
    {
        [Required(ErrorMessage = "Code System Parameter is a required field.")]
        [MaxLength(75, ErrorMessage = "Maximum length for the Code is 50 characters.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Content System Parameter is a required field.")]
        [MaxLength(75, ErrorMessage = "Maximum length for the Code is 50 characters.")]
        public string Content { get; set; }
        public string? Description { get; set; }
    }
}
