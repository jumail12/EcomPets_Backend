using Pets_Project_Backend.Data.Models.ProductModel;
using Pets_Project_Backend.Data.Models.UserModels;

namespace Pets_Project_Backend.Data.Models.CartModel
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }

        //nav
        public virtual ICollection<CartItem> _Items { get; set; }
        public virtual User _User { get; set; }
    }
}
