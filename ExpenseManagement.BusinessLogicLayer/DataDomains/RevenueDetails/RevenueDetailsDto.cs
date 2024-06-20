namespace ExpenseManagement.BusinessLogicLayer.DataDomains.RevenueDetails
{
    public class RevenueDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Mining { get; set; }
        public int BeforeMining { get; set; }
        public int Type { get; set; }
    }
}
