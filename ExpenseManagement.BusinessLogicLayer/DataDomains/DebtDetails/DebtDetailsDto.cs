namespace ExpenseManagement.BusinessLogicLayer.DataDomains.DebtDetails
{
    public class DebtDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Mining { get; set; }
        public int BeforeMining { get; set; }
        public int Type { get; set; }
        public bool IsPay { get; set; }
        public DateTime? DatePay { get; set; }
    }
}
