using System.ComponentModel.DataAnnotations;

namespace ExpenseManagement.BusinessLogicLayer.DataDomains.CategoriItem
{
    public class CategoryUpdateDto
    {
        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(75, ErrorMessage = "Maximum length for the Code is 50 characters.")]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
