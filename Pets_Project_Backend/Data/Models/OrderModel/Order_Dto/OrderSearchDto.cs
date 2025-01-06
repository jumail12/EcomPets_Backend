namespace Pets_Project_Backend.Data.Models.OrderModel.Order_Dto
{
    public class OrderSearchDto
    {
        public int? OrderId { get; set; }
        public string? OrderStatus { get; set; }
        public DateTime? OrderDatefirst { get; set; }
        public DateTime? OrderDateEnd { get; set; }
        public int? userId { get; set; }
    }
}
