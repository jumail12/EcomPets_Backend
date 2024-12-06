namespace Pets_Project_Backend.Data.Models.OrderModel.Order_Dto
{
    public class CreateOrder_Dto
    {
        public int AddId { get; set; }
        public decimal? Total { get; set; }
        public string? OrderString { get; set; }
        public string? TransactionId { get; set; }
    }
}
