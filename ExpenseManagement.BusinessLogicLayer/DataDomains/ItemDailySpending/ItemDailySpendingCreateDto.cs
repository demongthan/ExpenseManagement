using System.ComponentModel.DataAnnotations;

namespace ExpenseManagement.BusinessLogicLayer.DataDomains.ItemDailySpending
{
    public class ItemDailySpendingCreateDto
    {
        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(75, ErrorMessage = "Maximum length for the Code is 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mining is a required field.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a Mining bigger than 0")]
        public int Mining { get; set; }

        [Required(ErrorMessage = "Code Role is a required field.")]
        [Range(1, 255, ErrorMessage = "Please enter a Mining bigger than 0")]
        public int PaymentMethod { get; set; }

        public string? Description { get; set; }

        [Required]
        public Guid IdDailySpending { get; set; }

        public Guid IdCategory { get; set; }
    }
}
