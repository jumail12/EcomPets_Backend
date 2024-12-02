namespace Pets_Project_Backend.Data.Models.CartModel.Cart_Dtos
{
    public class CartView_Dto
    {
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
       
        public int? Price { get; set; }

        public string? ProductImage { get; set; }

        public int? TotalAmount { get; set; }

        public int? Quantity { get; set; }

    }
}
