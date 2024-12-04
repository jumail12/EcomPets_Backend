namespace Pets_Project_Backend.Data.Models.OrderModel.Order_Dto
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        public string ProductImage { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? OrderId { get; set; }
    }
}
