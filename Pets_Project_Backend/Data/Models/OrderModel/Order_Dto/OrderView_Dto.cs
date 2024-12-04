namespace Pets_Project_Backend.Data.Models.OrderModel.Order_Dto
{
    public class OrderView_Dto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
