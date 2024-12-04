namespace Pets_Project_Backend.Data.Models.OrderModel.Order_Dto
{
    public class CreateOrder_Dto
    {
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public decimal? Total { get; set; }
        public string? CustomerCity { get; set; }
        public string? HomeAddress { get; set; }
        public string? OrderString { get; set; }
        public string? TransactionId { get; set; }
    }
}
