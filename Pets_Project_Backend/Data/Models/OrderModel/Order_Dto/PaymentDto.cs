namespace Pets_Project_Backend.Data.Models.OrderModel.Order_Dto
{
    public class PaymentDto
    {
        public string? razorpay_payment_id { get; set; }
        public string? razorpay_order_id { get; set; }
        public string? razorpay_signature { get; set; }
    }
}
