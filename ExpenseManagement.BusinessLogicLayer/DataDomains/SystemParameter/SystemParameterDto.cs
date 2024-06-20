namespace ExpenseManagement.BusinessLogicLayer.DataDomains.SystemParameter
{
    public class SystemParameterDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Content { get; set; }
        public string? Description { get; set; }
    }
}
