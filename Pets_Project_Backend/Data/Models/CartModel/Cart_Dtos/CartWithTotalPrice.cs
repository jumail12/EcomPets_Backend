namespace Pets_Project_Backend.Data.Models.CartModel.Cart_Dtos
{
    public class CartWithTotalPrice
    {
        public int TotalCartPrice { get; set; }
        
        public List<CartView_Dto> c_items {  get; set; }
    }
}
