using ExpenseManagement.DataAccessLayer.DataModels.Common;

namespace ExpenseManagement.DataAccessLayer.DataModels
{
    public class ItemDailySpending : DateTimeCommon
    {
        public Guid Id { get; set; }
        public Guid IdDailySpending { get; set; }
        public DailySpending DailySpending { get; set; }
        public Guid IdCategory { get; set; }
        public CategoriesItem CategoriesItem { get; set; }
        public string Name { get; set; }
        public int Mining { get; set; }
        public int BeforeMining { get; set; }
        public int PaymentMethod { get; set; }
        public string? Description { get; set; }
    }
}
