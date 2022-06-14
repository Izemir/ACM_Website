namespace ACM_API.Models.Order
{
    public class OrderStatus
    {
        public long Id { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }
        public bool IsOrderVisible { get; set; }
    }
}
