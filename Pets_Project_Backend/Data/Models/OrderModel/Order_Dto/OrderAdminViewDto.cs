namespace Pets_Project_Backend.Data.Models.OrderModel.Order_Dto
{
    public class OrderAdminViewDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string OrderString { get; set; }
        public string CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public DateTime OrderDate { get; set; }
        public string TransactionId { get; set; }
    }
}
