namespace ExpenseManagement.BusinessLogicLayer.DataDomains.ItemDailySpending
{
    public class ItemDailySpendingDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Mining { get; set; }
        public int BeforeMining { get; set; }
        public int PaymentMethod { get; set; }
        public string? Description { get; set; }
    }
}
