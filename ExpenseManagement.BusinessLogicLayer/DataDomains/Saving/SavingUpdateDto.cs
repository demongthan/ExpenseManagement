using System.ComponentModel.DataAnnotations;

namespace ExpenseManagement.BusinessLogicLayer.DataDomains.Saving
{
    public class SavingUpdateDto
    {
        [Required(ErrorMessage = "CashSaving is a required field.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a Mining bigger than 0")]
        public int CashSaving { get; set; }

        [Required(ErrorMessage = "CreditSaving is a required field.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a Mining bigger than 0")]
        public int CreditSaving { get; set; }
    }
}
